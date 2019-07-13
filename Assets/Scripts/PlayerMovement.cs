using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 4;
    public Animator anim;
    public Rigidbody2D rgdb;

    private Vector2 m_inputDirection;

    private void Start()
    {

    }

    private void Update()
    {
        m_inputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (m_inputDirection != Vector2.zero)
        {
            anim.SetBool("isWalking", true);
            anim.SetFloat("movX", m_inputDirection.x);
            anim.SetFloat("movY", m_inputDirection.y);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        if (Input.GetKeyDown("space"))
        {
            anim.SetTrigger("isAttacking");
        }
    }

    private void FixedUpdate()
    {
        rgdb.MovePosition(rgdb.position + m_inputDirection.normalized * speed * Time.deltaTime);
    }
}
