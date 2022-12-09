using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject stagePanel;
    [SerializeField]
    private GameObject startButton;
    [SerializeField]
    private GameObject loadingPanel;

    public void onStartClicked()
    {
        stagePanel.SetActive(true);
        startButton.SetActive(false);
    }
    
    public void onBackClicked()
    {
        stagePanel.SetActive(false);
        startButton.SetActive(true);
    }

    public void onStageClicked(int num)
    {
        loadingPanel.SetActive(true);
        SceneManager.LoadScene("level_" + num);
    }
}
