using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Bosses : Enemy
{ 
    public GameObject portal;
    public Vector3 portalPosition = new Vector3();

    public void OpenPortal()
    {
        Instantiate(portal, portalPosition, transform.rotation);
    }
}
