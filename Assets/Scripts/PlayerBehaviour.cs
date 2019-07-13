using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float speed = 4;
    public Animator playerAnimator;
    public Rigidbody2D playerRigidbody;
	public Weapon currentWeapon;
	public GameObject currentArrow;

    private Vector2 m_inputDirection;
	private Vector2 m_lastDirection;

    private void Start()
    {
	}

    private void Update()
    {
		Movement();
		Attack();
    }

    private void FixedUpdate()
    {
        playerRigidbody.MovePosition(playerRigidbody.position + m_inputDirection.normalized * speed * Time.deltaTime);
    }

	private void Movement()
	{
		m_inputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

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

	private void Attack()
	{
		AnimatorStateInfo animInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);
		string triggerWeapon = "";
		string stateName = "";

		switch (currentWeapon.type)
		{
			case Weapon.Type.Sword:
				triggerWeapon = "isAttackingSword";
				stateName = "PlayerSwordAttack";
				break;
			case Weapon.Type.Bow:
				triggerWeapon = "isAttackingBow";
				stateName = "PlayerBowAttack";
				break;
		}

		bool attacking = animInfo.IsName(stateName);

		if (Input.GetKeyDown("space") && !attacking)
		{
			if (currentWeapon.type == Weapon.Type.Bow)
			{
				StartCoroutine(ThrowArrow());
			}
			playerAnimator.SetTrigger(triggerWeapon);
		}
	}

	public IEnumerator ThrowArrow()
	{
		yield return new WaitForSeconds(0.1f);
		GameObject arrowToThrow = Instantiate(currentArrow, gameObject.transform.position, Quaternion.identity);
		ArrowBehaviour arrowBehaviopur = arrowToThrow.GetComponent<ArrowBehaviour>();
		arrowBehaviopur.direction = m_lastDirection;
		yield return new WaitForSeconds(2f);
		Destroy(arrowToThrow);
	}
}
