using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField]
    private AudioSource jumpAudio;
    [SerializeField]
    private GameObject deathEffect;
    [SerializeField]
    private GameObject ball;
    [SerializeField] private AudioSource explosion;
    private bool isOnThorn;
    private bool isInFire;


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
        jumpAudio.Play();
        rigid.AddForce(Vector3.up * JumpConst*rigid.mass);
        StopCoroutine("JumpTimer");
        isSpacePressed = false;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Fire") || other.transform.CompareTag("Thorn"))
        {
            die();
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.transform.CompareTag("Ground"))
        {
            Debug.Log("ground");
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
        if (GameManager.instance.isEnd) return;
        GameManager.instance.onFail();
        explosion.Play();
        Instantiate(deathEffect, transform.position, transform.rotation);
        Instantiate(deathEffect, ball.transform.position, ball.transform.rotation);
        animator.Play("death");
        Destroy(ball);
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
