using UnityEngine;
using UnityEngine.SceneManagement; 

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] GameObject pauseMenuCanvas; 
    [SerializeField] GameObject controlsMenuCanvas; 
    [SerializeField] GameObject inventoryMenuCanvas; 

    public static bool isPaused = false; 
    void Update()
    {   Debug.Log("update frm pause menu calles"); 
        // Check for ESC key press
        if (Input.GetKeyDown(KeyCode.Escape) && !GameOverScript.instance.IsGameOver() && !VictoryMenuScript.instance.IsVictory())
        { 
            Debug.Log("Esc key pressed for pause");
            if (isPaused)
            {
                SoundMenuManager.instance.PauseButtonSound();
                Resume();
            } else
            {
                SoundMenuManager.instance.PauseButtonSound();
                Pause();
            }
        }
    }

    public void Pause() {
        pauseMenuCanvas.SetActive(true); 
        if (controlsMenuCanvas.activeSelf) {
            controlsMenuCanvas.SetActive(false); 
        }
        if (inventoryMenuCanvas.activeSelf) {
            inventoryMenuCanvas.SetActive(false); 
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
