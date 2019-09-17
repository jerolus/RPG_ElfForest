using UnityEngine;
using System.Collections;

public class WoogleEffect : MonoBehaviour {

	public float woogleAmount;
	public Vector3 woogleDirection;
	public float woogleDirChange;
	public float woogleFrequency;
	float currDir;
	Vector3 currDirV;
	// Use this for initialization
	void Start () {
		//woogleDirection = Vector3.up;
	}
	
	// Update is called once per frame
	void Update () {
		currDirV = Quaternion.Euler(0, 0, currDir) * woogleDirection;
		currDir += woogleDirChange * Time.deltaTime;
		transform.localScale = Vector3.one + (currDirV * (Mathf.Sin(Time.time * woogleFrequency) * woogleAmount));
	}
}
