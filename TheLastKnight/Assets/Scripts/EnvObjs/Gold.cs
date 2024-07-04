using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : Powerup
{
    public Inventory playerInventory; 
    void Start() {
        powerupSignal.Raise();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            playerInventory.goldCount += 1; 
            powerupSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}
