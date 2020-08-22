using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject main;
    public GameObject about;
    public Scene map1;
    public Text scoreBoard;
    private void Start()
    {
        Scores.Start();
    }
    private void Update()
    {
        string m = "";
        for(int i=0; i<10; i++)
        {
            m += (i+1).ToString() + " " + Scores.scoresData[i].nickname + " " + Scores.scoresData[i].points.ToString() + "\n";
        }
        scoreBoard.text = m;
    }
    public void startGame()
    {
        SceneManager.LoadScene(1);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));        
    }
    public void exitGame()
    {
        Application.Quit();
    }
    public void setMenu(bool value)
    {
        if(value)
        {
            main.SetActive(false);
            about.SetActive(true);
        }
        else
        {
            main.SetActive(true);
            about.SetActive(false);
        }
    }
}
