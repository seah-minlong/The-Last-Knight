using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 


public class VictoryMenuScript : MonoBehaviour
{
    [SerializeField] GameObject victoryMenuCanvas; 
    private static bool isVictory = false;

    public void Victory() {
        isVictory = true; 
        Invoke("Freeze", 4.0f);
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
