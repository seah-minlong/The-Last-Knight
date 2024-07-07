using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory Information")]
    public PlayerInventory playerInventory; 
    [SerializeField] private GameObject blankInventorySlot; 
    [SerializeField] private GameObject inventoryPanel; 
    [SerializeField] private TextMeshProUGUI descriptionText; 
    [SerializeField] private GameObject useButton; 
    public InventoryItem currentItem; 

    void OnEnable()
    {
        ClearInventorySlots(); 
        MakeInventorySlots(); 
        SetTextAndButton("", false); 
    }

    public void SetTextAndButton(string description, bool buttonActive) 
    {
        descriptionText.text = description;
        if (buttonActive)
        {
            useButton.SetActive(true); 
        } 
        else 
        {
            useButton.SetActive(false); 
        }
    }

    void MakeInventorySlots() 
    {
        if (playerInventory) 
        {
            for (int i =0; i < playerInventory.myInventory.Count; i++) 
            {
                if (playerInventory.myInventory[i].numberHeld > 0) 
                {
                    GameObject temp = Instantiate(blankInventorySlot, inventoryPanel.transform.position, Quaternion.identity);
                    temp.transform.SetParent(inventoryPanel.transform, false); 
                    InventorySlot newSlot = temp.GetComponent<InventorySlot>(); 
                
                    if (newSlot) 
                    {
                        newSlot.Setup(playerInventory.myInventory[i], this); 
                    }
                }
                
            }
        }
    }
   

    public void SetupDescriptionAndButton(string newDescriptionString, bool isButtonUsable, InventoryItem newItem)
    {
        currentItem = newItem; 
        descriptionText.text = newDescriptionString; 
        useButton.SetActive(isButtonUsable); 
    }

    void ClearInventorySlots() 
    {
        for(int i =0; i < inventoryPanel.transform.childCount; i++) 
        {
            Destroy(inventoryPanel.transform.GetChild(i).gameObject); 
        }
    }

    public void UseButtonPressed() {
        if (currentItem) 
        {
            currentItem.Use(); 
            //clear all inventory slots
            ClearInventorySlots(); 
            // then refill all slots with new numbers
            MakeInventorySlots();
            SetTextAndButton("", false); 
        }
    }
    
}
