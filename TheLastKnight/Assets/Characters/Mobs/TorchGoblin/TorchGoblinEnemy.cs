using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TorchGoblinEnemy : MobAIChase
{
    private void FixedUpdate() {
        AIChase(4);
    }
}
