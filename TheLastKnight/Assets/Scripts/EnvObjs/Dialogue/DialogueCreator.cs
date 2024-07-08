using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;


public class DialogueCreator : MonoBehaviour
{
    [Header("--------Dialogue box-------")]
    [SerializeField] protected GameObject dialogueBox; 
    [SerializeField] TextMeshProUGUI dialogueText; 
    [SerializeField] GameObject contextClue;
    public string dialogue; 
    protected bool playerInRange; 

    // Update is called once per frame
    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange)
        {
            DisableContextClue();
            if (dialogueBox.activeInHierarchy)
            {
                UpdateDialogue(); 
                dialogueBox.SetActive(false); 
            } else {
                UpdateDialogue(); 
                dialogueBox.SetActive(true); 
                dialogueText.text = dialogue; 
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player")) {
            EnableContextClue();
            playerInRange = true; 
            UpdateDialogue(); 
        }
    }

   private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.CompareTag("Player")) 
        {
            playerInRange = false;
            DisableContextClue();
            if (dialogueBox != null) 
            {
                dialogueBox.SetActive(false);
            }
        }
    }

    protected virtual void UpdateDialogue()
    {
        // This method can be overridden by subclasses to customize dialogue
    }

    private void EnableContextClue()
    {
        contextClue.SetActive(true);
    }

    private void DisableContextClue()
    {
        contextClue.SetActive(false);
    }

}
