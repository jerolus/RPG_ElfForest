using UnityEngine;
using System.Collections;

public class RotateOverTime : MonoBehaviour {

	public Vector3 rotation;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.localRotation = Quaternion.Euler(rotation) *  transform.localRotation;
	}
}
