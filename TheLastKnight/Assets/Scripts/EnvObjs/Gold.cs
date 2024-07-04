using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : Powerup
{
    public Inventory playerInventory; 
    [SerializeField] private AudioClip collectGoldClip;
    void Start() {
        powerupSignal.Raise();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            SoundFXManager.instance.PlaySoundFXClip(collectGoldClip, transform);
            playerInventory.goldCount += 1; 
            powerupSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}
