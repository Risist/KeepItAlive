using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mechanism : MonoBehaviour {
    [System.Serializable]
    public class StringProcedure : UnityEngine.Events.UnityEvent<string> {}
    [System.Serializable]
    public class IntProcedure : UnityEngine.Events.UnityEvent<int>{}
    [System.Serializable]
    public class Vector3Procedure : UnityEngine.Events.UnityEvent<Vector3>{}
    [System.Serializable]
    public class Vector2Procedure : UnityEngine.Events.UnityEvent<Vector2>{}
    [System.Serializable]
    public class NoArgProcedure : UnityEngine.Events.UnityEvent {}

    public StringProcedure invokeString;
    public string invokeStringDefaultParam;

    public IntProcedure invokeInt;
    public int invokeIntDefaultParam;

    public Vector3Procedure invokeVector3;
    public Vector3 invokeVector3DefaultParam;

    public Vector2Procedure invokeVector2;
    public Vector2 invokeVector2Param;

    public NoArgProcedure invoke;

    public void trip()
    {
        invokeString.Invoke(invokeStringDefaultParam);
        invokeInt.Invoke(invokeIntDefaultParam);
        invokeVector3.Invoke(invokeVector3DefaultParam);
        invokeVector2.Invoke(invokeVector2Param);
        invoke.Invoke();
    }


}
