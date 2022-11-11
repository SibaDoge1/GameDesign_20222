using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const float MoveConst = 1f;
    private const float JumpConst = 300.0f;
    private Rigidbody _rigid;
    private bool _isGrounded = false;
    private bool _isSpacePressed = false;
    
    // Start is called before the first frame update
    void Awake()
    {
        _rigid = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 nextMove = new Vector2();
        float horizental = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * horizental * Time.deltaTime * MoveConst, Space.World);
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopCoroutine("JumpTimer");
            StartCoroutine("JumpTimer");
        }
        if (_isSpacePressed && _isGrounded)
        {
            Jump();
        }
    }

    private void Jump()
    {
        _rigid.AddForce(Vector2.up * JumpConst);
        StopCoroutine("JumpTimer");
        _isSpacePressed = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Ground"))
        {
            _isGrounded = true;
            if(_isSpacePressed)
                Jump();
        }
    }
    
    private void OnCollisionExit(Collision other)
    {
        if (other.transform.CompareTag("Ground"))
            _isGrounded = false;
    }

    private IEnumerator JumpTimer()
    {
        float _timer = 0.1f;
        _isSpacePressed = true;

        while (_timer > 0)
        {
            _timer -= Time.deltaTime;
            yield return null;
        }
        _isSpacePressed = false;
    }
}
