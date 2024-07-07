using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;


public class InteractiveSign : MonoBehaviour
{
    [SerializeField] GameObject dialogueBox; 
    [SerializeField] TextMeshProUGUI dialogueText; 
    public string dialogue; 
    public bool playerInRange; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            if (dialogueBox.activeInHierarchy)
            {
                dialogueBox.SetActive(false); 
            } else {
                dialogueBox.SetActive(true); 
                dialogueText.text = dialogue; 
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player")) {
            playerInRange = true; 
            UpdateDialogue(); 
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.CompareTag("Player")) {
            playerInRange = false;  
            dialogueBox.SetActive(false); 
        }
    }

    protected virtual void UpdateDialogue()
    {
        // This method can be overridden by subclasses to customize dialogue
    }

}
