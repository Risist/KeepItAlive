using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightFadeout : MonoBehaviour
{
    /// when timer is ready light completely fades out
    /// so cd is just a time light has till fadeout
    public Timer fadeoutTimer;
    [Range(0, 1)] public float minLightDistance;
    ParticleSystem particle;
    new Light2D light;

    float initialLightIntensity;
    float initialParticleRate;
    float initialLightDistance;

    public void DecreaseLight(float f )
    {
        fadeoutTimer.actualTime -= f;
    }

    public float fadeoutPercent
    {
        get { return 1 - fadeoutTimer.GetCompletionPercent(); }
    }

    void Start()
    {
        light = GetComponentInChildren<Light2D>();
        particle = GetComponent<ParticleSystem>();
        var emission = particle.emission;

        initialLightIntensity = light.intensity;
        initialParticleRate = emission.rateOverTimeMultiplier;
        initialLightDistance = light.pointLightOuterRadius;
        
        Debug.Log("rate: " + initialParticleRate + ", intensity: " + initialLightIntensity);

        fadeoutTimer.Restart();
    }

    private void FixedUpdate()
    {
        float percent = fadeoutPercent;
        //percent = Mathf.Sqrt(percent);
        
        var emission = particle.emission;

        emission.rateOverTimeMultiplier = initialParticleRate * percent;
        light.pointLightOuterRadius = initialLightDistance * Mathf.Lerp(1.0f,percent, minLightDistance);

        Debug.Log("percent:" + percent + ", rate: " + emission.rateOverTimeMultiplier + ", intensity: " + light.intensity);
        light.intensity = initialLightIntensity * percent;
    }
}