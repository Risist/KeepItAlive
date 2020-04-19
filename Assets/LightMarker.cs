using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMarker : MonoBehaviour
{
    static readonly List<LightMarker> lights = new List<LightMarker>();

    public static Vector2 GetFleeDir(Vector2 target)
    {
        if (lights.Count == 0)
            return target;

        Vector2 offsetSum = Vector2.zero;
        foreach (var it in lights)
        {
            Vector2 toLight = target - (Vector2)it.transform.position;
            offsetSum += toLight / toLight.sqrMagnitude;
        }

        return offsetSum.normalized;
    }

    public static float GetDistanceSqToRandomLight(Vector2 position)
    {
        if (lights.Count == 0)
            return float.MaxValue;
        int lightId = Random.Range(0, lights.Count);
        Vector2 lightPosition = lights[lightId].transform.position;

        return (lightPosition - position).sqrMagnitude;
    }

    private void OnEnable()
    {
        lights.Add(this);
    }
    private void OnDisable()
    {
        lights.Remove(this);
    }
}
