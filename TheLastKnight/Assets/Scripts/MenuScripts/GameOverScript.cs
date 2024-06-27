using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.PackageManager;
using Unity.VisualScripting;
//using System.Numerics;

public class GameOverScript : MonoBehaviour
{
    [SerializeField] GameObject gameOverUI; 
    [SerializeField] AudioClip gameOverMusic;
    [SerializeField] PlayerController playerController; 
    private static bool isGameOver = false; 
    

    public void GameOver() 
    {
        SoundMenuManager.instance.ChangeBackgroundMusic(gameOverMusic);
        isGameOver = true; 
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
        isGameOver = false; 

        //reset respawnCount to 0 when moving to main menu 
        PlayerPrefs.SetInt("RespawnCount", 0); 
    }
    public void Respawn()
    {
        StartCoroutine(RespawnCoroutine());
    }

    private IEnumerator RespawnCoroutine()
    {
        // Hide game over UI and reset time scale
        gameOverUI.SetActive(false);
        Time.timeScale = 1;

         // Reset the game over state
        isGameOver = false;

        //Set increment respawnCount 
        PlayerPrefs.SetInt("RespawnCount", PlayerPrefs.GetInt("RespawnCount", 0) + 1); 


        // Store the checkpoint position
        Vector2 checkpointPosition = playerController.GetCheckpointPos();

        // Reload the current scene
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);

        // Wait until the scene is fully loaded
        yield return new WaitUntil(() => SceneManager.GetActiveScene().isLoaded);    
    }


    public bool IsGameOver() {
        return isGameOver; 
    }
}
