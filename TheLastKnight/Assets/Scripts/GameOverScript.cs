using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    [SerializeField] GameObject gameOverUI; 
    [SerializeField] GameObject pauseButton; 

    public void GameOver() {
        gameOverUI.SetActive(true); 
        pauseButton.SetActive(false); 
    }

    public void ToMainMenu(){

    SceneManager.LoadScene("MainMenuScene");


    }
}
