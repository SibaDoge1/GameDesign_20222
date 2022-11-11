using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -UnityEngine.Camera.main.transform.position.z);
        Vector3 lookPos =  UnityEngine.Camera.main.ScreenToWorldPoint(screenPos);
        Debug.Log(lookPos);
        transform.LookAt(new Vector3(lookPos.x, lookPos.y, transform.position.z));
        //(new Vector3(mousePos.x, mousePos.y, transform.position.z));
    }
}
