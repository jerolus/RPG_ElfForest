using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehaviour : MonoBehaviour
{
	public float fireRate = 1f;

	protected Animator m_animator;
	protected Rigidbody2D m_rigidbody;
	protected Vector2 m_direction;
	protected Transform m_target;
	protected bool m_canAttack = true;

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "Player" && !m_target)
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
