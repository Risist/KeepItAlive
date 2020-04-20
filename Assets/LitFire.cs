using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LitFire : MonoBehaviour, IDamagable
{
    public LightFadeout myFadeout;
    public float damageUnlitScale;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var lightFadeout = collision.gameObject.GetComponentInChildren<LightFadeout>();
        if (lightFadeout)
        {
            if (myFadeout)
            {
                lightFadeout.fadeoutTimer.actualTime = Mathf.Max(
                    lightFadeout.fadeoutTimer.actualTime,
                    myFadeout.fadeoutTimer.actualTime);
            }
        }
    }

    public void DealDamage(DamageData data)
    {
        if (data.damageType == EDamageType.EPhysical)
        {
            myFadeout.DecreaseLight(data.damage * damageUnlitScale);
        }
    }
}
