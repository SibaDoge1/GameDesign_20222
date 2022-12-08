using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;
    [SerializeField]
    private Player player;
    public Player Player => player;
    
    [SerializeField]
    private Ball ball;
    [SerializeField]
    private GameObject ballPrefab;
    [SerializeField]
    private GameObject spawnPoint;
    
    [SerializeField]
    private GameObject optionPanel;
    [SerializeField]
    private GameObject loadingPanel;
    
    [SerializeField]
    private AudioSource successSound;
    [SerializeField]
    private AudioSource failSound;
    [SerializeField]
    private AudioSource spawnSound;
    

    
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
        if(player == null)
            player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if(ball == null)
            ball = GameObject.FindWithTag("Ball").GetComponent<Ball>();
    }

    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            spawnSound.Play();
            GameObject newBall = Instantiate(ballPrefab, spawnPoint.transform.position, Quaternion.identity, ball.transform.parent);
            GameObject.Destroy(ball);
            ball = newBall.GetComponent<Ball>();
            spawnSound.Play();
        }
    }

    public void onSuccess()
    {
        successSound.Play();
        optionPanel.SetActive(true);
    }

    public void onFail()
    {
        failSound.Play();
    }

    public void onTitleClicked()
    {
        loadingPanel.SetActive(true);
        SceneManager.LoadScene("Main");
    }
}
