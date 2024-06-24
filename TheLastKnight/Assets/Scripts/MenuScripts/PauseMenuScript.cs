using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] GameObject pauseMenuCanvas; 
    [SerializeField] GameObject controlsMenuCanvas; 
    public static bool isPaused = false; 
    void Update()
    {
        
        // Check for ESC key press
        if (Input.GetKeyDown(KeyCode.Escape) && !FindObjectOfType<GameOverScript>().IsGameOver() && !FindObjectOfType<VictoryMenuScript>().IsVictory())
        { 
            Debug.Log("Esc key pressed for pause");
            
            if (isPaused)
            {
                SoundMenuManager.instance.PauseSound();
                Resume();
            } else
            {
                SoundMenuManager.instance.PauseSound();
                Pause();
            }
        }
    }

    public void Pause() {
        pauseMenuCanvas.SetActive(true); 
        if (controlsMenuCanvas.activeSelf) {
            controlsMenuCanvas.SetActive(false); 
        }
        Time.timeScale = 0;
        isPaused = true;
        SoundMenuManager.instance.PauseMusic();
    }

    public void Resume() {
        pauseMenuCanvas.SetActive(false); 
        Time.timeScale = 1; 
        isPaused = false;
        SoundMenuManager.instance.ResumeMusic();
    }

    public void MainMenu() {
        SceneManager.LoadScene("MainMenuScene"); 
        Time.timeScale = 1; 
        isPaused = false; 
    }
    
}
