using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : InteractiveSign
{
    [SerializeField] InventoryItem playerItem; 
    [SerializeField] string itemSoldByPawn;
    [SerializeField] int costOfItem; 
    [SerializeField] GameObject buyButton; 
    [SerializeField] MySignal buyItemSignal; 
    
    

    protected override void UpdateDialogue()
    {
        base.UpdateDialogue();

        int playerItemHeld = playerItem.numberHeld;
        string playerItemName = playerItem.itemName; 

        if (playerItemHeld >= costOfItem)
        {
            dialogue = $"You have {playerItemHeld} {playerItemName}. Would you like to buy one {itemSoldByPawn} for {costOfItem} {playerItemName}?";
            buyButton.SetActive(true); 
        }
        else
        {
            dialogue = $"Sorry, you do not have enough {playerItemName} to buy a {itemSoldByPawn}.";
            buyButton.SetActive(false); 
        }
    }
}
