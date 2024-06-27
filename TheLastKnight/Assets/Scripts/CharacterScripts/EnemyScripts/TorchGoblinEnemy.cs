using UnityEngine;

public class TorchGoblinEnemy : MobAIChase
{
    [SerializeField] private Powerup heart;
    [SerializeField] private AudioClip damageSoundClip;
    
    private void FixedUpdate() {
        AIChase(getChaseRadius(), getAttackRadius());
    }

    public override void TookDamage(float damage) {
        
        health -= damage;
        if(alive)
        {
            // play sound FX 
            SoundFXManager.instance.PlaySoundFXClip(damageSoundClip, transform);

            if (health <= 0)
            {
                LockMovement();
                Defeated();
                alive = false;
            } else 
            {
                Stagger();
            }
        } 
        else 
        {
            LockMovement();
            Defeated();
        }
    }

    public void dropHeart()
    {
        // chance that heart will drop 20%
        int rand = Random.Range(0, 100);
        if (rand <= 10) 
        {
            Instantiate(heart, transform.position, transform.rotation);
        }
    }
}