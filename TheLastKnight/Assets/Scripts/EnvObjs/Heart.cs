using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Powerup
{
    [SerializeField] FloatValue playerHealth;
    [SerializeField] FloatValue heartContainers;
    [SerializeField] float amountToIncrease;
    [SerializeField] private AudioClip collectHeartClip;

    public void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            float maxHealth = heartContainers.initialValue * 2f;
            // No effect if player at max health
            if (playerHealth.RuntimeValue != maxHealth)
            {
                // play sound FX 
                SoundFXManager.instance.PlaySoundFXClip(collectHeartClip, transform);

                playerHealth.RuntimeValue += amountToIncrease;
                if (playerHealth.RuntimeValue > maxHealth)
                {
                    playerHealth.RuntimeValue = maxHealth;
                }
                powerupSignal.Raise();
                Destroy(this.gameObject);
            }
        }
    }
}
