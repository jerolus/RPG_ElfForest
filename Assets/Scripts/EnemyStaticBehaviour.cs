using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStaticBehaviour : MonoBehaviour
{
	public GameObject projectilePrefab;
	public float m_fireRate = 1;

	private Animator m_animator;
	private Rigidbody2D m_rigidbody;
	private Vector2 m_direction;
	private Transform m_target;
	private bool m_canAttack = true;
	private float m_minFireRate = 0.4f;

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

	public void CheckAttack()
	{
		AnimatorStateInfo animInfo = m_animator.GetCurrentAnimatorStateInfo(0);

		if (m_canAttack)
		{
			StartCoroutine(ThrowProjectile());
		}
	}

	private void CheckDirection()
	{
		m_direction = m_target.transform.position - m_rigidbody.transform.position;
		float angle = Mathf.Atan2(m_direction.y, m_direction.x) * Mathf.Rad2Deg - 90;
		m_rigidbody.rotation = angle;
	}

	public IEnumerator ThrowProjectile()
	{
		m_canAttack = false;
		m_animator.Play("EnemyAttack");
		yield return new WaitForSeconds(0.1f);
		GameObject projectileToThrow = Instantiate(projectilePrefab, gameObject.transform.position, Quaternion.identity);
		ProjectileBehaviour projectileBehaviopur = projectileToThrow.GetComponent<ProjectileBehaviour>();
		projectileBehaviopur.direction = m_direction;
		yield return new WaitForSeconds(m_fireRate);
		m_canAttack = true;
		yield return new WaitForSeconds(1f);
		Destroy(projectileToThrow);
	}

	public void SetFireRate(float newFireRate)
	{
		if (newFireRate >= m_minFireRate)
		{
			m_fireRate = newFireRate;
		}
		else
		{
			m_fireRate = m_minFireRate;
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			m_target = other.transform;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			m_target = null;
		}
	}
}
