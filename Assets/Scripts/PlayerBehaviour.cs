using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float speed = 4;
    public Animator playerAnimator;
    public Rigidbody2D playerRigidbody;

    private Vector2 m_inputDirection;

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
		bool attacking = animInfo.IsName("PlayerAttack");

		if (Input.GetKeyDown("space") && !attacking)
		{
			playerAnimator.SetTrigger("isAttacking");
		}
	}
}
