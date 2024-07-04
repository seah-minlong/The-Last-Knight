using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldCountText;
    [SerializeField] Inventory playerInventory; 

    void Start() {
        playerInventory.goldCount = 0; 
        UpdateGoldCountText(); 
    }

    public void UpdateGoldCountText()
    {
        goldCountText.text = "" + playerInventory.goldCount;
    }
}
