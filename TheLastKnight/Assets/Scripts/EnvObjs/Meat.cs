using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meat : Powerup
{
    [SerializeField] private AudioClip collectMeatClip;

    public void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            SoundFXManager.instance.PlaySoundFXClip(collectMeatClip, transform);
        }
    }
}
