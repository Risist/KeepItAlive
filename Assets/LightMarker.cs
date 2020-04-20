using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightMarker : MonoBehaviour
{
    static readonly List<LightMarker> lights = new List<LightMarker>();
    new Light2D light;

    private void Start()
    {
        light = GetComponentInChildren<Light2D>();
    }

    public static Vector2 GetFleeDir(Vector2 target)
    {
        if (lights.Count == 0)
            return target;

        Vector2 offsetSum = Vector2.zero;
        foreach (var it in lights)
        {
            Vector2 toLight = target - (Vector2)it.transform.position;
            offsetSum += toLight / toLight.sqrMagnitude * it.light.intensity;
        }

        return offsetSum.normalized;
    }

    public static float GetDistanceSqToRandomLight(Vector2 position)
    {
        if (lights.Count == 0)
            return float.MaxValue;
        int lightId = Random.Range(0, lights.Count);
        Vector2 lightPosition = lights[lightId].transform.position;

        float light = lights[lightId].light.intensity;
        if (light < 0.001)
            return float.MaxValue;
        else
            return (lightPosition - position).sqrMagnitude / light;
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
