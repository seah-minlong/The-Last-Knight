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
    [SerializeField] protected AudioClip damageSoundClip;
    [SerializeField] protected AudioClip deathSoundClip;

    [Header("--------Battle---------")]
    [SerializeField] protected Enemy enemy;
    
    protected List<Vector3> spawnPositionList;
    protected float halfHealth;
    protected bool Stage2 = false;
    
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

    public void playDeathSound()
    {
        // play sound FX 
        SoundFXManager.instance.PlaySoundFXClip(deathSoundClip, transform);
    }

    protected void SpawnEnemy()
    {
        for (int i = 0; i < spawnPositionList.Count; i++) 
        {
            Vector3 spawnPosition = spawnPositionList[i];
            Instantiate(enemy, spawnPosition, Quaternion.identity);
        }
    }
}