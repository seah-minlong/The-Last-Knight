using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalInventoryItem : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory; 
    [SerializeField] private InventoryItem thisItem; 
    public MySignal receivedItemSignal; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            AddItemToInventory(); 
             if (thisItem.itemName == "Gold")
            {
                receivedItemSignal.Raise(); //for the sound
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

