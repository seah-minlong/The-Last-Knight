using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : InteractiveSign
{
    [SerializeField] InventoryItem playerItem; 
    [SerializeField] string itemSoldByPawn;
    [SerializeField] int costOfItem; 
    [SerializeField] MySignal boughtFromPawnSignal; 

    [SerializeField] FloatValue healthContainers; 
    [SerializeField] GameObject pawnDialogueBox; 
    private int playerItemHeld;
    private string playerItemName;

   

    new void Update() 
    {
        base.Update(); 
        playerItemHeld = playerItem.numberHeld;
        playerItemName = playerItem.itemName;
        if (Input.GetKeyDown(KeyCode.B) && playerItemHeld >= costOfItem && healthContainers.RuntimeValue < 5 && pawnDialogueBox.activeSelf)
        {
            boughtFromPawnSignal.Raise(); 
        }
    }
    
    

    protected override void UpdateDialogue()
    {
        base.UpdateDialogue();

        playerItemHeld = playerItem.numberHeld;
        playerItemName = playerItem.itemName; 

        if (playerItemName == "Gold" && healthContainers.RuntimeValue == 5) 
        {
            dialogue = "You have reached the max number of Health Containers!"; 
        }
        else if (playerItemHeld >= costOfItem)
        {
            dialogue = $"You have {playerItemHeld} {playerItemName}. Would you like to buy one {itemSoldByPawn} for {costOfItem} {playerItemName}? Press 'B' to buy.";
        }
        else 
        {
            dialogue = $"Sorry, you do not have enough {playerItemName} to buy a {itemSoldByPawn}.";
        }
    }
}