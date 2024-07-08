using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnShop : DialogueCreator
{
    [Header("--------Pawn Shop-------")]
    [SerializeField] InventoryItem playerItem; 
    [SerializeField] string itemSoldByPawn;
    [SerializeField] int costOfItem; 
    [SerializeField] MySignal boughtFromPawnSignal; 
    [SerializeField] FloatValue healthContainers; 
    [SerializeField] private AudioClip increaseHeartClip;

    private int playerItemHeld;
    private string playerItemName;

    private new void Update() 
    {
        base.Update(); 
        playerItemHeld = playerItem.numberHeld;
        playerItemName = playerItem.itemName;
        if (Input.GetKeyDown(KeyCode.B) && playerItemHeld >= costOfItem && healthContainers.RuntimeValue < 5 && dialogueBox.activeSelf)
        {
            boughtFromPawnSignal.Raise(); 
            dialogueBox.SetActive(false);

            // play sound FX 
            SoundFXManager.instance.PlaySoundFXClip(increaseHeartClip, transform);
        }
    }
    
    protected override void UpdateDialogue()
    {
        playerItemHeld = playerItem.numberHeld;
        playerItemName = playerItem.itemName; 

        if (playerItemName == "Gold" && healthContainers.RuntimeValue == 5) 
        {
            dialogue = "You have reached the max number of Health Containers!"; 
        }
        else if (playerItemHeld >= costOfItem)
        {
            dialogue = $"You have {playerItemHeld} {playerItemName}. Would you like to purchase {itemSoldByPawn} for {costOfItem} {playerItemName}? Press 'B' to buy.";
        }
        else 
        {
            dialogue = $"Sorry, you do not have enough {playerItemName} to purchase {itemSoldByPawn}.";
        }
    }
}