using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAngleOverTime : MonoBehaviour {

	public Vector3 startAngle;
	public Vector3 endAngle;
	public float speed;
	public bool local;
	float timer;
	Quaternion start;
	Quaternion end;

	void Start()
	{
		start = Quaternion.Euler(startAngle);
		end = Quaternion.Euler(endAngle);
	}
	// Update is called once per frame
	void Update () {
		float pos = 0.5f + (Mathf.Sin(timer * speed) * 0.5f);
		if (local)
		{
			transform.localRotation = Quaternion.Lerp(start, end, pos);
		}else
		{
			transform.rotation = Quaternion.Lerp(start, end, pos);
		}
		timer += Time.deltaTime;
	}
}
