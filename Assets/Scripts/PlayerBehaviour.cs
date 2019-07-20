using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
	private static PlayerBehaviour m_instance;

	public float speed = 4;
	public GameObject arrowPrefab;
    public Animator playerAnimator;

	private bool m_canMove = true;
	private Vector2 m_inputDirection;
	private Vector2 m_lastDirection;
	private InventorySystem m_inventory;
    private Rigidbody2D m_playerRigidbody;

	#region Awake Start Update FixedUpdate
	private void Awake()
	{
		SetInstance();
	}

	private void Start()
    {
		m_inventory = InventorySystem.GetInstance();
		m_playerRigidbody = GetComponent<Rigidbody2D>();
	}

	private void Update()
    {
		CheckInput();
		CheckMovementAnimations();
		CheckAttack();
    }

    private void FixedUpdate()
    {
		if (m_canMove)
		{
			m_playerRigidbody.MovePosition(m_playerRigidbody.position + m_inputDirection.normalized * speed * Time.deltaTime);
		}
    }
	#endregion

	#region Instance
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
	#endregion

	#region Movement
	private void CheckInput()
	{
		m_inputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
	}


	private void CheckMovementAnimations()
	{
		if (m_inputDirection != Vector2.zero && m_canMove)
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
	#endregion

	#region Attack
	private void CheckAttack()
	{
		if (m_inventory.currentWeapon && m_canMove)
		{
			AnimatorStateInfo animInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);
			string triggerWeapon = "";
			string stateName = "";

			switch (m_inventory.currentWeapon.type)
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
				if (m_inventory.currentWeapon.type == InventoryItem.Type.Bow)
				{
					if (m_inventory.GetStacksNumber(InventoryItem.Type.Arrow) > 0)
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
		m_inventory.RemoveStackType(InventoryItem.Type.Arrow);
		yield return new WaitForSeconds(0.1f);
		GameObject arrowToThrow = Instantiate(arrowPrefab, gameObject.transform.position, Quaternion.identity);
		ArrowBehaviour arrowBehaviopur = arrowToThrow.GetComponent<ArrowBehaviour>();
		arrowBehaviopur.direction = m_lastDirection;
		yield return new WaitForSeconds(2f);
		Destroy(arrowToThrow);
	}
	#endregion

	public IEnumerator TeleportTransition(Transform transformToMove, string textToShow)
	{
		GameController.GetInstance().DoFade();
		m_canMove = false;
		yield return new WaitForSeconds(0.6f);
		transform.position = transformToMove.position;
		GameController.GetInstance().ChangeTextFade(textToShow);
		yield return new WaitForSeconds(1f);
		m_canMove = true;
	}
}
