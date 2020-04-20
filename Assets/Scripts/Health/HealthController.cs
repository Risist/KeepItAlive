using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthController : MonoBehaviour, IDamagable
{
    [Header("Health")]
    public float currentHealth = 100;
    public float maxHealth = 100;

    public bool destroyOnDeath = true;

    public Action<DamageData> onDamageCallback;
    public Action<DamageData> onDeathCallback;
    public Action<DamageData> onStaggerCallback;

    [Header("Stagger")]
    public Timer tResetStagger;
    public float staggerLimit;
    public float staggerLevel;

    bool destroyed = false;

    public void Ressurect()
    {
        destroyed = false;
        currentHealth = maxHealth;
    }

    public void DealDamage(float damage, float stagger)
    {
        DamageData data = new DamageData();
        data.damage = damage;
        data.staggerIncrease = stagger;
        DealDamage(data);
    }
    public void DealDamage(DamageData data)
    {
        currentHealth -= data.damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        staggerLevel += data.staggerIncrease;


        if (data.staggerIncrease > 0)
        {
            tResetStagger.Restart();
        }

        if(staggerLevel >= staggerLimit)
        {   
            staggerLevel -= staggerLimit;
            onStaggerCallback(data);
        }

        if (currentHealth > 0 && onDamageCallback != null)
            onDamageCallback(data);
        else if (!destroyed)
        {
            if (onDeathCallback != null)
                onDeathCallback(data);
            destroyed = true;
        }
    }

    void Start()
    {
        onStaggerCallback += (DamageData data) => { };
        onDamageCallback += (DamageData data) => { };
        onDeathCallback += (DamageData data) => {
            if(destroyOnDeath)
                Destroy(gameObject);
        };
    }

    void Update()
    {
        if(tResetStagger.IsReadyRestart())
        {
            staggerLevel = Mathf.Clamp(staggerLevel - 1, 0, int.MaxValue);
        }
    }
}
