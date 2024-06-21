using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsMenuScript : MonoBehaviour
{
    [SerializeField] GameObject controlsMenuCanvas; 
    void Update() {
        // Check for ESC key press
        if (Input.GetKeyDown(KeyCode.Escape)) { 
                controlsMenuCanvas.SetActive(false); 
        }
    }
}
