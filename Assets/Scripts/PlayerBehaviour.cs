using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
	private static PlayerBehaviour m_instance;

	public float speed = 4f;
	public GameObject arrowPrefab;
    public Animator animator;
	public Slider slide;

	private int m_life = 100;
	private bool m_canMove = true;
	private Vector2 m_direction;
	private Vector2 m_mousePos;
    private Rigidbody2D m_rigidbody;
	private GameController m_controller;
	private Camera m_camera;

	#region Awake Start Update FixedUpdate
	private void Awake()
	{
		SetInstance();
	}

	private void Start()
    {
		m_rigidbody = GetComponent<Rigidbody2D>();
		m_controller = GameController.GetInstance();
		m_camera = Camera.main;
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
			m_rigidbody.MovePosition(m_rigidbody.position + m_direction.normalized * speed * Time.deltaTime);
			Vector2 lookDir = m_mousePos - m_rigidbody.position;
			float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;
			m_rigidbody.rotation = angle;
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
		m_direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		m_mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
	}

	private void CheckMovementAnimations()
	{
		if (m_direction != Vector2.zero && m_canMove)
		{
			animator.SetBool("isWalking", true);
		}
		else
		{
			animator.SetBool("isWalking", false);
		}
	}
	#endregion

	#region Attack
	private void CheckAttack()
	{
		if (m_canMove)
		{
			if (Input.GetKeyDown("space"))
			{
				StartCoroutine(ThrowArrow());
				animator.SetTrigger("isAttackingBow");
			}
		}
	}

	public IEnumerator ThrowArrow()
	{
		yield return new WaitForSeconds(0.1f);
		GameObject arrowToThrow = Instantiate(arrowPrefab, gameObject.transform.position, m_rigidbody.transform.rotation);
		ProjectileBehaviour arrowBehaviopur = arrowToThrow.GetComponent<ProjectileBehaviour>();
		arrowBehaviopur.direction = m_rigidbody.transform.up;
		yield return new WaitForSeconds(2f);
		Destroy(arrowToThrow);
	}
	#endregion

	#region Life
	public int GetLife()
	{
		return m_life;
	}

	public void SetLife(int newValue)
	{
		m_life = newValue;
		if (m_life > 100)
		{
			m_life = 100;
		}
		else if(m_life <= 0)
		{
			m_controller.MainMenu();
			Debug.Log("DIED");
		}
		slide.value = m_life;
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

	#region Collisions
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "EnemyAttack")
		{
			SetLife(m_life - GameController.ENEMY_DAMAGE);
		}
	}
	#endregion
}
