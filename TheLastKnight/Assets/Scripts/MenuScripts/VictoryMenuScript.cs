using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement; 


public class VictoryMenuScript : MonoBehaviour
{
    [SerializeField] GameObject victoryMenuCanvas; 
    [SerializeField] AudioClip victoryMusic;
    [SerializeField] GameObject dialogueBoxCanvas; 
    private static bool isVictory = false;
    public static VictoryMenuScript instance;

    void Start() 
    {
        isVictory = false; 
    }

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
        dialogueBoxCanvas.SetActive(false); 
        victoryMenuCanvas.SetActive(true); 
    }

    public void Freeze() {
        Time.timeScale = 0; 
    }

    public bool IsVictory() {
        return isVictory; 
    }

    public void ToMainMenu() {
        PlayerPrefs.SetInt("RespawnCount", 0); 
        SceneManager.LoadScene("MainMenuScene"); 
        Time.timeScale = 1; 
        isVictory = false; 
    }

    public static bool GetIsVictory() {
        return isVictory; 
    }
}
