using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : Powerup
{
    [SerializeField] private AudioClip collectGoldClip;

    public void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            SoundFXManager.instance.PlaySoundFXClip(collectGoldClip, transform);
        }
    }
}
