using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;


public class InteractiveSign : MonoBehaviour
{
    [SerializeField] GameObject dialogueBox; 
    [SerializeField] TextMeshProUGUI dialogueText; 
<<<<<<< Updated upstream
    public string dialogue; 
    public bool playerInRange; 
=======
<<<<<<< Updated upstream
    [SerializeField] string dialogue; 
    [SerializeField] bool playerInRange; 
>>>>>>> Stashed changes
    // Start is called before the first frame update
    void Start()
    {
        
    }

=======
    public string dialogue; 
    public bool playerInRange; 
    
>>>>>>> Stashed changes
    // Update is called once per frame
    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
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
