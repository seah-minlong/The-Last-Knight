using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class SignalListener : MonoBehaviour {

    public MySignal signal;
    public UnityEvent signalEvent;

    public void OnSignalRaised() 
    {
        signalEvent.Invoke();
    }

    private void OnEnable()
    {
        signal.RegisterListener(this);
    }

    private void OnDisable()
    {
        signal.DeRegisterListener(this);
    }
}