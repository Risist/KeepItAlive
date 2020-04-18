using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mechanism : MonoBehaviour {
    [System.Serializable]
    public class Querry : UnityEngine.Events.UnityEvent<string> { 
        //implements UnityEvent that 
    }
    public Querry callback;
    public string dynamicQuerry;

    void trip()
    {
        callback.Invoke(dynamicQuerry);
    }

}
