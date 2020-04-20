using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EDamageType
{
    EPhysical,
    EFire,
};

[System.Serializable]
public class DamageData
{
    public float damage;
    public float staggerIncrease;
    public EDamageType damageType = EDamageType.EPhysical;
    [HideInInspector] public Vector3 position;
    [HideInInspector] public Vector3 direction;
}

/// interface for all objects that can be damaged
public interface IDamagable
{
    void DealDamage(DamageData data);
}
