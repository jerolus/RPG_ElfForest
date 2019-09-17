using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinusOverTime : MonoBehaviour
{
    public Vector3 direction;
    public Vector3 center;
    public float frequency;
    public float offset;
    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = center + (direction * Mathf.Sin(offset + (Time.time * frequency)));
    }
}
