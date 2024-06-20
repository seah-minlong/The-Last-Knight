using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu; 

    public void Pause() {
        pauseMenu.SetActive(true); 
        Time.timeScale = 0;
    }

    public void Resume() {
        pauseMenu.SetActive(false); 
        Time.timeScale = 1; 
    }

    public void MainMenu() {
        SceneManager.LoadScene("MainMenuScene"); 
        Time.timeScale = 1; 

    }
}
