using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 5;
    [SerializeField] private float forceConst = 250f;
    [SerializeField] private float maxForce = 2.0f;

 
    public float gravityScale = 1.0f;
    public static float globalGravity = -9.81f;
    
    private bool isForceOk;

    private Rigidbody _rigidbody;
    private bool isMouseHolding;
    private Vector3 mouseClickPos;
    private float forceCoolDown;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource hitAudio;
    [SerializeField] private AudioSource forceAudio;
    [SerializeField] private AudioSource explosion;
    private LineRenderer line;
    [SerializeField] private GameObject linePrefab;
    [SerializeField]
    private GameObject hitEffect;
    // Start is called before the first frame update
    void Start()
    {
        isForceOk = true;
        _rigidbody = transform.GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        if (GameManager.instance.isEnd) return;
        if (Input.GetMouseButtonDown(0) && isForceOk)
        {
            isMouseHolding = true;
            mouseClickPos = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition);
            line = Instantiate(linePrefab, new Vector3(mouseClickPos.x,mouseClickPos.y,transform.position.z), Quaternion.identity).GetComponent<LineRenderer>();
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            if (isMouseHolding && isForceOk)
            {
                Vector2 differ = mouseClickPos - UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 dir = differ.normalized;
                float manitude = differ.magnitude;
                _rigidbody.AddForce(dir* Math.Min(manitude, maxForce)*_rigidbody.mass*forceConst);
                isForceOk = false;
                forceAudio.Play();
            }
            isMouseHolding = false;
            if(line != null)
                Destroy(line.gameObject);
            
        }

        if (isMouseHolding)
        {
            line?.SetPosition(1, Vector3.ClampMagnitude(UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition) - mouseClickPos, maxForce));
        }
        _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, maxSpeed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        _rigidbody.AddForce(gravity, ForceMode.Acceleration);
        if (GameManager.instance.isEnd) return;
        _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, maxSpeed);
    }

    public void die()
    {
        GameManager.instance.onFail();
        animator.Play("death");
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Racket"))
        {
            isForceOk = true;
        }

        if (other.gameObject.CompareTag("Racket") || other.gameObject.CompareTag("Ground"))
        {
            hitAudio.Play();
            Instantiate(hitEffect, transform.position, transform.rotation);
        }
    }
}
