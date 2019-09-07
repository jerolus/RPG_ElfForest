using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
	public Vector2 direction;
	public float speed = 6;

	private Rigidbody2D m_rigigbody;

	private void Start()
	{
		m_rigigbody = gameObject.GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
    {
		m_rigigbody.MovePosition(m_rigigbody.position + direction.normalized * speed * Time.deltaTime);
	}
}
