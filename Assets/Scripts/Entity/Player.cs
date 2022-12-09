using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const float MoveConst = 1.25f;
    private const float JumpConst = 75.0f;
    private Rigidbody rigid;
    private bool isGrounded = false;
    private bool isSpacePressed = false;
    private Animator animator; 
    
    // Start is called before the first frame update
    void Awake()
    {
        rigid = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isEnd) return;
        Vector2 nextMove = new Vector2();
        float horizental = Input.GetAxis("Horizontal");
        rigid.MovePosition(rigid.position + Vector3.right * horizental * Time.deltaTime * MoveConst);
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopCoroutine("JumpTimer");
            StartCoroutine("JumpTimer");
        }
        if (isSpacePressed && isGrounded)
        {
            Jump();
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    private void Jump()
    {
        rigid.AddForce(Vector2.up * JumpConst*rigid.mass);
        StopCoroutine("JumpTimer");
        isSpacePressed = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Ground"))
        {
            isGrounded = true;
            if(isSpacePressed)
                Jump();
        }
    }
    
    private void OnCollisionExit(Collision other)
    {
        if (other.transform.CompareTag("Ground"))
            isGrounded = false;
    }

    public void die()
    {
        animator.Play("death");
    }

    private IEnumerator JumpTimer()
    {
        float _timer = 0.1f;
        isSpacePressed = true;

        while (_timer > 0)
        {
            _timer -= Time.deltaTime;
            yield return null;
        }
        isSpacePressed = false;
    }
}
