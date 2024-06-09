using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver {

    public float initialValue;
    
    [HideInInspector]
    public float RuntimeValue;

    public void OnAfterDeserialize() 
    {
        RuntimeValue = initialValue;
    }

    public void OnBeforeSerialize() {}

}