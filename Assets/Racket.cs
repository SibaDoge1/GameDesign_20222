using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket : MonoBehaviour
{
    private Rigidbody rigid;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // if (other.gameObject.CompareTag("ball"))
        // {
        //     Debug.Log(rigid.velocity);
        //     other.gameObject.GetComponent<Rigidbody>().AddForce(rigid.velocity*rigid.mass);
        // }
    }
}
