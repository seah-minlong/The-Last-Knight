using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Silence : MonoBehaviour
{
    [Header("--------Silence Zone---------")]
    [SerializeField] private ColliderTrigger colliderTrigger;

    // Start is called before the first frame update
    private void Start()
    {
        colliderTrigger.OnPlayerEnterTrigger += ColliderTrigger_OnPlayerEnterTrigger;
    }

    private void ColliderTrigger_OnPlayerEnterTrigger(object sender, System.EventArgs e)
    {
        Debug.Log("Silence");
        SoundMenuManager.instance.FadeOutAndPauseMusic();
        colliderTrigger.OnPlayerEnterTrigger -= ColliderTrigger_OnPlayerEnterTrigger;
    }
}
