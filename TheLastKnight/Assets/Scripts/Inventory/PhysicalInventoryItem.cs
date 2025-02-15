using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalInventoryItem : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory; 
    [SerializeField] private InventoryItem thisItem; 
    public MySignal receivedItemSignal; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            AddItemToInventory(); 
             if (thisItem.itemName == "Gold")
            {
                Debug.Log("inside gold inven item"); 
                receivedItemSignal.Raise(); 
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

