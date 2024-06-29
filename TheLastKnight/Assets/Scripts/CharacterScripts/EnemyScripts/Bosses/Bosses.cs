using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

abstract public class Bosses : Enemy
{ 
    [Header("----------Portal-----------")]
    [SerializeField] GameObject portal;
    [SerializeField] Vector3 portalPosition = new Vector3();

    [Header("--------SoundFX------")]
    [SerializeField] private AudioClip damageSoundClip;
    [SerializeField] private AudioClip deathSoundClip;

    [Header("--------Battle---------")]
    [SerializeField] private Enemy enemy;
    
    private List<Vector3> spawnPositionList;
    private float halfHealth;
    private bool Stage2 = false;
    
    private void Awake()
    {
        halfHealth = getMaxHealth() / 2;
        spawnPositionList = new List<Vector3>();

        foreach (Transform spawnPosition in transform.Find("spawnPositions"))
        {
            spawnPositionList.Add(spawnPosition.position);
        }
    }

    public void OpenPortal()
    {
        Instantiate(portal, portalPosition, transform.rotation);
    }
    
    public override void TookDamage(float damage) {
        
        health -= damage;

        // play sound FX 
        SoundFXManager.instance.PlaySoundFXClip(damageSoundClip, transform);

        if(alive)
        {
            if (!Stage2 && health <= halfHealth)
            {
                Debug.Log("Stage 2");
                Stage2 = true;

                // Play Stage 2 Music
                BossMusic.instance.Stage2Music();

                SpawnEnemy();   
            }
            else if (health <= 0)
            {
                // Stop Music
                SoundMenuManager.instance.PauseMusic();

                LockMovement();
                Defeated();
                alive = false;
            } 
            else
            {
                Stagger();
            }
        } 
    }

    public void playDeathSound()
    {
        // play sound FX 
        SoundFXManager.instance.PlaySoundFXClip(deathSoundClip, transform);
    }

    private void SpawnEnemy()
    {
        for (int i = 0; i < spawnPositionList.Count; i++) 
        {
            Vector3 spawnPosition = spawnPositionList[i];
            Instantiate(enemy, spawnPosition, Quaternion.identity);
        }
    }
}