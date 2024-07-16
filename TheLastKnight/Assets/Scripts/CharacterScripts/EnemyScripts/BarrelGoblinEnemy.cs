using UnityEngine;

public class BarrelGoblinEnemy : MobAIChase
{
    [SerializeField] private Powerup heart;
    [SerializeField] private Powerup gold; 
    [SerializeField] private AudioClip damageSoundClip;
    
    private void FixedUpdate() {
        if (alive) 
        {
            AIChase(getChaseRadius(), getAttackRadius());
        }
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
            } 
        } 
    }

    public void Explode() 
    {
        animator.SetTrigger("explode"); 
    }

    public void DropItem()
    {
        // chance that heart will drop 20%
        int rand = Random.Range(0, 100);
        if (rand <= 10) 
        {
            Instantiate(heart, transform.position, transform.rotation);
        } else if (rand <= 20) {
            Instantiate(gold, transform.position, transform.rotation); 
        }
    }
}
