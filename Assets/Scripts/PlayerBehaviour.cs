using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
	private static PlayerBehaviour m_instance;

	public float speed = 4;
    public Animator playerAnimator;
    public Rigidbody2D playerRigidbody;

	public GameObject currentArrow;

	public List<InventoryItem> inventory = new List<InventoryItem>();
	public Transform inventoryParent;
	public InventoryItem currentWeapon;

    private Vector2 m_inputDirection;
	private Vector2 m_lastDirection;

	private void Awake()
	{
		SetInstance();
	}

	private void Start()
    {
	}

	public static PlayerBehaviour GetInstance()
	{
		if (m_instance != null)
		{
			return m_instance;
		}
		else
		{
			return null;
		}
	}

	private void SetInstance()
	{
		if (!m_instance)
		{
			m_instance = this;
		}
	}

	private void Update()
    {
		CheckInput();
		CheckMovementAnimations();
		CheckAttack();
    }

    private void FixedUpdate()
    {
        playerRigidbody.MovePosition(playerRigidbody.position + m_inputDirection.normalized * speed * Time.deltaTime);
    }

	private void CheckInput()
	{
		m_inputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
	}


	private void CheckMovementAnimations()
	{
		if (m_inputDirection != Vector2.zero)
		{
			m_lastDirection = m_inputDirection;
			playerAnimator.SetBool("isWalking", true);
			playerAnimator.SetFloat("movX", m_inputDirection.x);
			playerAnimator.SetFloat("movY", m_inputDirection.y);
		}
		else
		{
			playerAnimator.SetBool("isWalking", false);
		}
	}

	private void CheckAttack()
	{
		if (currentWeapon)
		{
			AnimatorStateInfo animInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);
			string triggerWeapon = "";
			string stateName = "";

			switch (currentWeapon.type)
			{
				case InventoryItem.Type.Sword:
					triggerWeapon = "isAttackingSword";
					stateName = "PlayerSwordAttack";
					break;
				case InventoryItem.Type.Bow:
					triggerWeapon = "isAttackingBow";
					stateName = "PlayerBowAttack";
					break;
			}

			bool attacking = animInfo.IsName(stateName);

			if (Input.GetKeyDown("space") && !attacking)
			{
				if (currentWeapon.type == InventoryItem.Type.Bow)
				{
					if (GetTypeNumber(InventoryItem.Type.Arrow) > 0)
					{
						StartCoroutine(ThrowArrow());
						playerAnimator.SetTrigger(triggerWeapon);
					}
				}
				else
				{
					playerAnimator.SetTrigger(triggerWeapon);
				}
			}
		}
	}

	public IEnumerator ThrowArrow()
	{
		RemoveArrow();
		yield return new WaitForSeconds(0.1f);
		GameObject arrowToThrow = Instantiate(currentArrow, gameObject.transform.position, Quaternion.identity);
		ArrowBehaviour arrowBehaviopur = arrowToThrow.GetComponent<ArrowBehaviour>();
		arrowBehaviopur.direction = m_lastDirection;
		yield return new WaitForSeconds(2f);
		Destroy(arrowToThrow);
	}

	public void AddInventoryItem(InventoryItem item)
	{
		item.transform.SetParent(inventoryParent);
		inventory.Add(item);
		if (item.type == InventoryItem.Type.Bow || item.type == InventoryItem.Type.Sword)
		{
			currentWeapon = item;
		}
	}

	private int GetTypeNumber(InventoryItem.Type typeToCompare)
	{
		int numberToReturn = 0;
		for (int i = 0; i < inventory.Count; i++)
		{
			if (inventory[i].type == typeToCompare)
			{
				numberToReturn++;
			}
		}
		return numberToReturn;
	}

	private void RemoveArrow()
	{
		for (int i = 0; i < inventory.Count; i++)
		{
			if (inventory[i].type == InventoryItem.Type.Arrow)
			{
				inventory.Remove(inventory[i]);
			}
		}
	}
}
