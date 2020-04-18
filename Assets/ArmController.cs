using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    public float angleMin;
    public float angleMax;


    float GetAngle()
    {
        return transform.eulerAngles.z;
    }
    void SetAngle(float v)
    {
        transform.eulerAngles = new Vector3(0, 0, v);
    }

    void Start()
    {   

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //Quaternion.Euler();
        //SetAngle(Mathf.Clamp(GetAngle(), angleMin, angleMax));
        Debug.Log(Mathf.Clamp(GetAngle(), -75, 10));
        //SetAngle(ClampAngle(GetAngle() + 180, angleMin+180, angleMax + 180) - 180);
    }

    float ClampAngle(float angle, float from, float to)
    {
        // accepts e.g. -80, 80
        if (angle < 0f) angle = 360 + angle;
        if (angle > 180f) return Mathf.Max(angle, 360 + from);
        return Mathf.Min(angle, to);
    }
}
