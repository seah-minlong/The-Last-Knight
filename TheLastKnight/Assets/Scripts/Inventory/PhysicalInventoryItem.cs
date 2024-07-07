using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalInventoryItem : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory; 
    [SerializeField] private InventoryItem thisItem; 
    public MySignal receivedItemSignal; 
<<<<<<< Updated upstream
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
=======
>>>>>>> Stashed changes

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            AddItemToInventory(); 
             if (thisItem.itemName == "Gold")
            {
<<<<<<< Updated upstream
                Debug.Log("inside gold inven item"); 
                receivedItemSignal.Raise(); 
=======
                receivedItemSignal.Raise(); //for the sound
>>>>>>> Stashed changes
            }
            Destroy(this.gameObject); 
        }
    }
    

    void AddItemToInventory()
    {
        if (playerInventory && thisItem) 
        {
            if (playerInventory.myInventory.Contains(thisItem))
            {
                thisItem.numberHeld++; 
            } 
            else 
            {
                playerInventory.myInventory.Add(thisItem);
                thisItem.numberHeld++; 
            }
        }
    }
}

