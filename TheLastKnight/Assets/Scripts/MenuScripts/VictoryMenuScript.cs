using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 


public class VictoryMenuScript : MonoBehaviour
{
    [SerializeField] GameObject victoryMenuCanvas; 
    [SerializeField] AudioClip victoryMusic;
    private static bool isVictory = false;
    public static VictoryMenuScript instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Victory() {
        SoundMenuManager.instance.ChangeBackgroundMusic(victoryMusic);
        isVictory = true; 
        Freeze(); 
        victoryMenuCanvas.SetActive(true); 
    }

    public void Freeze() {
        Time.timeScale = 0; 
    }

    public bool IsVictory() {
        return isVictory; 
    }

    public void ToMainMenu() {
        SceneManager.LoadScene("MainMenuScene"); 
        Time.timeScale = 1; 
        isVictory = false; 
    }
}
