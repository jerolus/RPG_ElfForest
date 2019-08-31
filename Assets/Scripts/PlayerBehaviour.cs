using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
	private static PlayerBehaviour m_instance;

	public float speed = 4;
	public int life = 100;
	public GameObject arrowPrefab;
    public Animator playerAnimator;

	private bool m_canMove = true;
	private Vector2 m_inputDirection;
	private Vector2 m_lastDirection;
    private Rigidbody2D m_playerRigidbody;
	private GameController m_controller;

	#region Awake Start Update FixedUpdate
	private void Awake()
	{
		SetInstance();
	}

	private void Start()
    {
		m_playerRigidbody = GetComponent<Rigidbody2D>();
		m_controller = GameController.GetInstance();
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

			if (m_inputDirection != Vector2.zero)
			{
				float angle = Mathf.Atan2(m_inputDirection.y, m_inputDirection.x) * Mathf.Rad2Deg - 90;
				transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
			}
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
		if (m_canMove)
		{
			AnimatorStateInfo animInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);

			bool attacking = animInfo.IsName("PlayerBowAttack");

			if (Input.GetKeyDown("space") && !attacking)
			{
				StartCoroutine(ThrowArrow());
				playerAnimator.SetTrigger("isAttackingBow");
			}
		}
	}

	public IEnumerator ThrowArrow()
	{
		yield return new WaitForSeconds(0.1f);
		GameObject arrowToThrow = Instantiate(arrowPrefab, gameObject.transform.position, Quaternion.identity);
		ProjectileBehaviour arrowBehaviopur = arrowToThrow.GetComponent<ProjectileBehaviour>();
		arrowBehaviopur.direction = m_lastDirection;
		yield return new WaitForSeconds(2f);
		Destroy(arrowToThrow);
	}
	#endregion

	public IEnumerator TeleportTransition(Transform transformToMove, string textToShow)
	{
		m_controller.DoFade();
		m_canMove = false;
		yield return new WaitForSeconds(0.6f);
		transform.position = transformToMove.position;
		m_controller.ChangeTextFade(textToShow);
		yield return new WaitForSeconds(1f);
		m_canMove = true;
	}

	#region Life
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "EnemyAttack")
		{
			life -= GameController.ENEMY_DAMAGE;
			if (life <= 0)
			{
				Debug.Log("DIED");
				Destroy(this.gameObject);
			}
		}
	}
	#endregion
}
