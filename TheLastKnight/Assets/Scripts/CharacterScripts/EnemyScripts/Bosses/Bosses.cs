using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Bosses : Enemy
{ 
    [Header("Portal")]
    [SerializeField] GameObject portal;
    [SerializeField] Vector3 portalPosition = new Vector3();

    public void OpenPortal()
    {
        Instantiate(portal, portalPosition, transform.rotation);
    }
}