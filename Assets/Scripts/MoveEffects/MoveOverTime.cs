using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOverTime : MonoBehaviour
{
    public Vector3 velocity;
    public float totalTime;
    public bool pingPong;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = totalTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer > 0)
        {
            transform.position += velocity * Time.deltaTime;
        }else
        {
            if (pingPong)
            {
                velocity *= -1f;
                timer = totalTime;
            }
        }

    }
}
