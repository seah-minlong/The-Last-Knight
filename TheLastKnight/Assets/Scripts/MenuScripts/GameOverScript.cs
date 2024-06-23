using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    [SerializeField] GameObject gameOverUI; 
    [SerializeField] AudioClip gameOverMusic;

    public void GameOver() 
    {
        SoundMenuManager.instance.ChangeBackgroundMusic(gameOverMusic);
        gameOverUI.SetActive(true); 
        Invoke("Freeze", 0.6f);
    }

    public void Freeze() 
    {
        Time.timeScale = 0; 
    }
    
    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
        Time.timeScale = 1; 
    }
}
