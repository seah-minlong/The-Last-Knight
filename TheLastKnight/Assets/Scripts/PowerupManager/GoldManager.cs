using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldCountText;
    [SerializeField] InventoryItem goldItem; 


    void Start() {
        Debug.Log("start for gold manager is called"); 
        if (PlayerController.GetRespawnCount() == 0 && !PlayerController.GetIsNextLevel())
        { 
            goldItem.numberHeld = 0; 
        }
        UpdateGoldCountText(); 
    }

    public void UpdateGoldCountText()
    {
        Debug.Log("update gold text"); 
        goldCountText.text = "" + goldItem.numberHeld;
    }
}
