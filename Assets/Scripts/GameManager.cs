using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;
    private Player player;
    public Player Player => player;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            UnityEngine.Debug.LogError("SingleTone Error : " + this.name);
            Destroy(this);
        }
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        
    }
}
