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
		CheckRotation();
	}

	private void FixedUpdate()
    {
		m_rigigbody.MovePosition(m_rigigbody.position + direction.normalized * speed * Time.deltaTime);
	}

	private void CheckRotation()
	{
		float toRotate = 0;
		if (direction.x == 0 && direction.y == 1)
		{
			toRotate = 0;
		}
		else if (direction.x == 1 && direction.y == 1)
		{
			toRotate = -45;
		}
		else if (direction.x == 1 && direction.y == 0)
		{
			toRotate = -90;
		}
		else if (direction.x == 1 && direction.y == -1)
		{
			toRotate = -135;
		}
		else if (direction.x == 0 && direction.y == -1)
		{
			toRotate = 180;
		}
		else if (direction.x == -1 && direction.y == -1)
		{
			toRotate = 135;
		}
		else if (direction.x == -1 && direction.y == 0)
		{
			toRotate = 90;
		}
		else if (direction.x == -1 && direction.y == 1)
		{
			toRotate = 45;
		}

		transform.rotation = Quaternion.Euler(0, 0, toRotate);
	}
}
