using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

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
    private GameObject successPanel;
    [SerializeField]
    private GameObject failPanel;
    [SerializeField]
    private GameObject loadingPanel;
    [SerializeField]
    private GameObject optionPanel;
    
    [SerializeField]
    private AudioSource successSound;
    [SerializeField]
    private AudioSource failSound;
    [SerializeField]
    private AudioSource spawnSound;

    public bool isEnd;
    

    
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
            onRestartClicked();
            /*spawnSound.Play();
            GameObject.Destroy(ball.gameObject);
            GameObject newBall = Instantiate(ballPrefab, spawnPoint.transform.position, Quaternion.identity, ball.transform.parent);
            ball = newBall.GetComponent<Ball>();
            spawnSound.Play();*/
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            optionPanel.SetActive(true);
        }
    }

    public void onSuccess()
    {
        isEnd = true;
        successSound.Play();
        successPanel.SetActive(true);
    }

    public void onFail()
    {
        isEnd = true;
        failSound.Play();
        failPanel.SetActive(true);
    }
    
    public void onRestartClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void onResumeClicked()
    {
        Time.timeScale = 1;
        optionPanel.SetActive(false);
    }


    public void onTitleClicked()
    {
        loadingPanel.SetActive(true);
        SceneManager.LoadScene("Main");
    }
}
