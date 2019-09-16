using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStaticBehaviour : EnemyBehaviour
{
	public GameObject projectilePrefab;

	private void Start()
	{
		m_rigidbody = gameObject.GetComponent<Rigidbody2D>();
		m_animator = gameObject.GetComponent<Animator>();
	}

	private void Update()
	{
		if (m_target)
		{
			CheckDirection();
			CheckAttack();
		}
	}

	private void CheckAttack()
	{
		if (m_canAttack)
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
		yield return new WaitForSeconds(0.1f);
		GameObject projectileToThrow = Instantiate(projectilePrefab, gameObject.transform.position, Quaternion.identity);
		ProjectileBehaviour projectileBehaviopur = projectileToThrow.GetComponent<ProjectileBehaviour>();
		projectileBehaviopur.direction = m_direction;
		yield return new WaitForSeconds(fireRate - 0.1f);
		m_canAttack = true;
		yield return new WaitForSeconds(1f);
		Destroy(projectileToThrow);
	}
}
