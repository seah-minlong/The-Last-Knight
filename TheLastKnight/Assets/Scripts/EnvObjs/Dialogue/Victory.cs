using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryPawn : DialogueCreator
{
    private new void Update() 
    {
        base.Update(); 
        if (!VictoryMenuScript.instance.IsVictory() && Input.GetKeyDown(KeyCode.V))
        {
            VictoryMenuScript.instance.Victory();
        }
    }
}