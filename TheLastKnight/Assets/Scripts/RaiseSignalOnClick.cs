using UnityEngine;
using UnityEngine.UI;

public class RaiseSignalOnClick : MonoBehaviour
{
    public MySignal clickedSignal;
    public void RaisedSignal()
    {
        Debug.Log("signal raised"); 
        if (clickedSignal!= null)
        {
            clickedSignal.Raise();
        }
    }
}
