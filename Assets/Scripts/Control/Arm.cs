using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
    //[SerializeField] private Transform realArm;
    [SerializeField] private Rigidbody racketRigid;
    [SerializeField] private Transform fakeRacket;
    [SerializeField] private Transform test;
    [SerializeField] private Transform maxPoint;
    [SerializeField] private Transform minPoint;
    private Vector3 lookPos;
    
    // Start is called before the first frame update
    void Start()
    {
        racketRigid.maxAngularVelocity = 1f;
    }

    private void Update()
    {        
        transform.rotation = Quaternion.RotateTowards(transform.rotation,Quaternion.LookRotation(lookPos- transform.position,Vector3.back), 10f);
        fakeRacket.position = getClampedVector(lookPos, minPoint.position, maxPoint.position);
        test.position = lookPos;
    }
    private void FixedUpdate()
    {
        Vector3 screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -UnityEngine.Camera.main.transform.position.z);
        Vector3 worldPos =  UnityEngine.Camera.main.ScreenToWorldPoint(screenPos);
        lookPos = new Vector3(worldPos.x, worldPos.y, transform.position.z);
        


        racketRigid.MovePosition(fakeRacket.position);
        racketRigid.MoveRotation(fakeRacket.rotation);
        //racketRigid.MovePosition(Vector3.Lerp(racketRigid.position,getClampedVector(lookPos, minPoint.position, maxPoint.position), Time.fixedTime));
        //racketRigid.MoveRotation(transform.rotation);
        //racketRigid.velocity = Vector3.ClampMagnitude(racketRigid.velocity, 1f);
        //racketRigid.angularVelocity = Vector3.ClampMagnitude(racketRigid.angularVelocity, 1f);
        //_rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, maxSpeed);
        //realArm.position = (getClampedVector(lookPos, minPoint.position, maxPoint.position));
        //realArmRigid2.MoveRotation(realArmRigid.rotation);
        //(new Vector3(mousePos.x, mousePos.y, transform.position.z));
    }

    Vector3 getClampedVector(Vector3 target, Vector3 min, Vector3 max)
    {
        return new Vector3(Math.Clamp(target.x, Math.Min(min.x,max.x), Math.Max(min.x,max.x)) , 
                        Math.Clamp(target.y, Math.Min(min.y,max.y), Math.Max(min.y,max.y)), 
                        transform.position.z);
    }
}
