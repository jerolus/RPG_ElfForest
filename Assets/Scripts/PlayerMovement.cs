using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6;
    public Animator anim;
    public Rigidbody2D rgdb;

    private Vector2 m_inputDirection;

    private void Start()
    {

    }

    private void Update()
    {
        m_inputDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (m_inputDirection != Vector2.zero)
        {
            anim.SetBool("walking", true);
            anim.SetFloat("movX", m_inputDirection.x);
            anim.SetFloat("movY", m_inputDirection.y);
        }
        else
        {
            anim.SetBool("walking", false);
        }
    }

    private void FixedUpdate()
    {
        rgdb.MovePosition(rgdb.position + m_inputDirection.normalized * speed * Time.deltaTime);
    }
}
