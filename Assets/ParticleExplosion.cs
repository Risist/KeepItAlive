using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ParticleExplosion : MonoBehaviour
{
    public Timer tDestroy;
    public Timer tReverse;
    [Range(0, 1)] public float lightLerpFactor = 0.1f;
    ParticleSystem system;
    Light2D light;
    float initialIntensity;

    private void Start()
    {
        light = GetComponentInChildren<Light2D>();
        initialIntensity = light.intensity;
        light.intensity = 0;

        tReverse.Restart();
        tDestroy.Restart();
    }
    private void FixedUpdate()
    {
        light.intensity = Mathf.Lerp(light.intensity, tReverse.IsReady() ? 0 : initialIntensity, lightLerpFactor);

        if (tDestroy.IsReady())
            Destroy(gameObject);
    }
}
