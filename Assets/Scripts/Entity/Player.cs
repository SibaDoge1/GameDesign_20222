using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float MoveConst = 5.25f;
    [SerializeField]
    private float JumpConst = 150.0f;
    private Rigidbody rigid;
    private bool isGrounded = false;
    private bool isSpacePressed = false;
    private Animator animator;
    private float horizental;
    private bool isInFire;
    private bool isOnThorn;


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
        horizental = Input.GetAxis("Horizontal");
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopCoroutine("JumpTimer");
            StartCoroutine("JumpTimer");
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

    private void FixedUpdate()
    {
        
        if (isSpacePressed && isGrounded)
        {
            Jump();
        }
        if(horizental != 0)
            rigid.MovePosition(transform.position+Vector3.right * horizental * Time.fixedDeltaTime * MoveConst);
    }

    private void Jump()
    {
        rigid.AddForce(Vector3.up * JumpConst*rigid.mass);
        StopCoroutine("JumpTimer");
        isSpacePressed = false;
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.transform.CompareTag("Ground"))
        {
            isGrounded = true;
            if(isSpacePressed)
                Jump();
        }
        else if (other.transform.CompareTag("Fire") || other.transform.CompareTag("Thorn"))
        {
            GameManager.instance.onFail();
            die();
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
