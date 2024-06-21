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
    private static bool isPaused = false; 
    void Update()
    {
        
        // Check for ESC key press
        if (Input.GetKeyDown(KeyCode.Escape))
        { 
            Debug.Log("Esc key pressed for pause");
            
            if (isPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Pause() {
        pauseMenuCanvas.SetActive(true); 
        Time.timeScale = 0;
        isPaused = true;
    }

    public void Resume() {
        pauseMenuCanvas.SetActive(false); 
        Time.timeScale = 1; 
        isPaused = false;
    }

    public void MainMenu() {
        SceneManager.LoadScene("MainMenuScene"); 
        Time.timeScale = 1; 
        isPaused = false; 
    }
}
