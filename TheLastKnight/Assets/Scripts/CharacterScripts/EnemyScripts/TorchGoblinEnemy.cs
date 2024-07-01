using UnityEngine;

public class TorchGoblinEnemy : MobAIChase
{
    [SerializeField] private Powerup heart;
    [SerializeField] private AudioClip damageSoundClip;
    
    private void FixedUpdate() {
        AIChase(getChaseRadius(), getAttackRadius());
    }

    public override void TookDamage(float damage) {
        if(alive)
        {
            health -= damage;

            // play sound FX 
            SoundFXManager.instance.PlaySoundFXClip(damageSoundClip, transform);

            if (health <= 0)
            {
                alive = false;
                LockMovement();
                Defeated();
            } else 
            {
                Stagger();
            }
        } 
        else if (canMove)
        {
            // Enemy still can move even though movement should be locked
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