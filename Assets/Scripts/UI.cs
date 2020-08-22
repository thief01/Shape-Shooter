using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public Text scoreDisplayer;
    public Text scoreDisplayerOnDeathScreen;
    public Text healthDisplayer;
    public Text nickHolder;

    public Image[] slotWeapons;
    public Image[] weapons;
    public Text[] weaponsAmmoDisplayer;
    public Player player;
    public GameObject pauseMenu;
    private void Start()
    {
        Time.timeScale = 1;    
    }
    void Update()
    { 
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale == 1)
            {
                pauseGame();
            }
            else
            {
                resumeGame();
            }
        }
        scoreDisplayer.text = player.scores.ToString();
        scoreDisplayerOnDeathScreen.text = scoreDisplayer.text;
        healthDisplayer.text = player.health.ToString();
        updateWeaponUI();
    }
    void updateWeaponUI()
    {
        for (int i = 0; i < slotWeapons.Length; i++)
        {
            weaponsAmmoDisplayer[i].text = player.weapons[i].ammo.ToString();
            slotWeapons[i].gameObject.SetActive(player.weapons[i].avaible);
            slotWeapons[i].color = new Color32(255,93,0,255);
        }
        slotWeapons[player.usingWeapon].color = new Color(255, 235, 0);
    }
    public void saveScore()
    {
        Scores.addNewScore(nickHolder.text, player.scores);
        SceneManager.LoadScene(0);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));
    }
    public void backToMenu()
    {
        SceneManager.LoadScene(0);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));
    }
    public void resumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }
    public void pauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }
}
