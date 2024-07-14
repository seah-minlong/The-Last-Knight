using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public static SceneController instance; 

    private void Awake() {
        if (instance == null) {
                instance = this; 
                DontDestroyOnLoad(gameObject); 
        } else {
            Destroy(gameObject); 
        }
    }
    
    public void NextLevel() {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1); 
    }

    public void LoadScene(String sceneName) {
        SceneManager.LoadSceneAsync(sceneName); 
    }
}
