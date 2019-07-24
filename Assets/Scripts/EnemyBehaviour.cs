using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
	public Animator enemyAnimator;
	public GameObject projectilePrefab;

	private Vector2 m_direction;
	private Transform m_target;
	private bool m_canAttack = true;

	private void Start()
	{
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
		AnimatorStateInfo animInfo = enemyAnimator.GetCurrentAnimatorStateInfo(0);
		bool attacking = animInfo.IsName("EnemyAttack");

		if (!attacking && m_canAttack)
		{
			StartCoroutine(ThrowProjectile());
		}
	}

	private void CheckDirection()
	{
		m_direction = (m_target.position - transform.position).normalized;
		enemyAnimator.SetFloat("dirX", m_direction.x);
		enemyAnimator.SetFloat("dirY", m_direction.y);
	}

	public IEnumerator ThrowProjectile()
	{
		m_canAttack = false;
		enemyAnimator.Play("EnemyAttack");
		yield return new WaitForSeconds(0.1f);
		GameObject projectileToThrow = Instantiate(projectilePrefab, gameObject.transform.position, Quaternion.identity);
		ProjectileBehaviour arrowBehaviopur = projectileToThrow.GetComponent<ProjectileBehaviour>();
		arrowBehaviopur.direction = m_direction;
		yield return new WaitForSeconds(1f);
		m_canAttack = true;
		yield return new WaitForSeconds(1f);
		Destroy(projectileToThrow);
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
