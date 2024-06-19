using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    [SerializeField] GameObject gameOverUI; 

    public void GameOver() {
        gameOverUI.SetActive(true); 
    }

    public void ToMainMenu(){

    SceneManager.LoadScene("MainMenuScene");


    }
}
