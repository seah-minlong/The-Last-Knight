using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGame : MonoBehaviour
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

   public static void ResetGameLevel()
    {
        Debug.Log("reset game level called"); 
        PlayerController.SetIsNextLevel(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetInventory(); 
        ResetGameLevel(); 
    }
}
