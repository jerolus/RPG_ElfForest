using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
	public Transform target;

	private Camera m_camera;

    private void Start()
    {
		m_camera = gameObject.GetComponent<Camera>();
    }

    private void Update()
    {
		if (target)
		{
			transform.position = Vector3.Lerp(transform.position, target.position, 0.05f) + new Vector3(0, 0, -10);
		}

    }
}
