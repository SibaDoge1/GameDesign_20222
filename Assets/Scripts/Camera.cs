using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = GameManager.instance.Player.transform.position;
        transform.position = new Vector3(playerPos.x+5, playerPos.y, transform.position.z);
    }
}
