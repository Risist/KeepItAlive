using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class OilController : MonoBehaviour, IDamagable
{
    [Range(0, 1)] public float lightLerp = 0.05f;
    public float spreadFireRadius = 5.0f;
    public float spreadFireDelay = 1.0f;
    public LayerMask spreadLayer;
    float desiredLightIntensity;


    new ParticleSystem particleSystem;
    new Light2D light;

    float initialLightIntensity;
    
    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>(true);
        light = GetComponentInChildren<Light2D>(true);
        initialLightIntensity = light.intensity;
        light.intensity = 0;
        desiredLightIntensity = 0;
    }
    private void FixedUpdate()
    {
        light.intensity = Mathf.Lerp(light.intensity, desiredLightIntensity, lightLerp);
        if (!particleSystem.isEmitting)
        {
            particleSystem.gameObject.SetActive(false);
            desiredLightIntensity = 0;
        }
    }

    void SpreadFire()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, spreadFireRadius, spreadLayer);
        foreach (var it in colliders)
        {
            var oilController = it.GetComponent<OilController>();
            if (oilController)
            {
                oilController.Lit();
            }
        }

    }

    public void DealDamage(DamageData data)
    {
        if (data.damageType == EDamageType.EFire)
        {
            Lit();
        }
    }

    void Lit()
    {
        if (particleSystem.isPlaying)
            return;

        particleSystem.gameObject.SetActive(true);
        particleSystem.Play();
        desiredLightIntensity = initialLightIntensity;
        Invoke("SpreadFire", spreadFireDelay);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireSphere(Vector3.zero, spreadFireRadius);
    }
}