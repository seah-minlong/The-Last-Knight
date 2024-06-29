using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMusic : MonoBehaviour
{
    [Header("--------Audio---------")]
    [Tooltip("AudioClips have to be preloaded or there will be a lag")]
    [SerializeField] private AudioClip BossBattleMusicStage1;
    [SerializeField] private AudioClip BossBattleMusicStage2;

    [Header("--------Boss Zone---------")]
    [SerializeField] private ColliderTrigger colliderTrigger;
    public static BossMusic instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        colliderTrigger.OnPlayerEnterTrigger += ColliderTrigger_OnPlayerEnterTrigger;
    }

    private void ColliderTrigger_OnPlayerEnterTrigger(object sender, System.EventArgs e)
    {
        StartBattle();
        colliderTrigger.OnPlayerEnterTrigger -= ColliderTrigger_OnPlayerEnterTrigger;
    }
    
    private void StartBattle()
    {
        Debug.Log("StartBattle");

        // Change music when boss battle starts
        SoundMenuManager.instance.FadeOutAndChangeMusic(BossBattleMusicStage1);
    }

    public void Stage2Music()
    {
        SoundMenuManager.instance.FadeOutAndChangeMusic(BossBattleMusicStage2);
    }
}
