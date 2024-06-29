using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTrigger : MonoBehaviour
{
    public event EventHandler OnPlayerEnterTrigger;

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        PlayerController player = collider.GetComponent<PlayerController>();
        if (player != null)
        {
            // Player inside trigger area
            OnPlayerEnterTrigger?.Invoke(this, EventArgs.Empty);
        }
    }
}
