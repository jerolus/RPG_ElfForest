using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementBehaviour : EnemyBehaviour
{
	public float speed = 2f;
	public GameObject attackCollider;

	private void Start()
	{
		m_rigidbody = gameObject.GetComponent<Rigidbody2D>();
		m_animator = gameObject.GetComponent<Animator>();
	}

	private void Update()
	{
		if (m_target)
		{
			CheckMovement();
			CheckDirection();
			CheckAttack();
		}
	}

	private void CheckMovement()
	{
		if ((m_target.transform.position - transform.position).magnitude > 0.5f)
		{
			m_rigidbody.MovePosition(m_rigidbody.position + m_direction.normalized * speed * Time.deltaTime);
		}
	}

	private void CheckAttack()
	{
		if (m_canAttack && (m_target.transform.position - transform.position).magnitude < 0.9f)
		{
			StartCoroutine(Attack());
		}
	}

	private void CheckDirection()
	{
		m_direction = m_target.transform.position - m_rigidbody.transform.position;
		float angle = Mathf.Atan2(m_direction.y, m_direction.x) * Mathf.Rad2Deg - 90;
		m_rigidbody.rotation = angle;
	}

	private IEnumerator Attack()
	{
		m_canAttack = false;
		m_animator.Play("EnemyAttack");
		attackCollider.SetActive(true);
		yield return new WaitForSeconds(0.2f);
		attackCollider.SetActive(false);
		yield return new WaitForSeconds(fireRate - 0.2f);
		m_canAttack = true;
	}
}
