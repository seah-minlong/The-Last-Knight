using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerInventoryOnPlay : MonoBehaviour
{
    [SerializeField] PlayerInventory playerInventory; 

    public void ResetInventory()
    {
        Debug.Log("ResetInventory called"); 
        if (playerInventory == null)
        {
            Debug.LogError("PlayerInventory is not assigned!");
            return;
        }

        foreach (InventoryItem item in playerInventory.myInventory)
        {
            if (item == null)
            {
                Debug.LogError("InventoryItem is null in playerInventory.");
                continue;
            }
            item.numberHeld = 0; 
        }
    } 
    // Start is called before the first frame update
    void Start()
    {
        ResetInventory(); 
    }
}
