using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float damage;
    void OnTriggerEnter2D(Collider2D col) {
        DamageData data = new DamageData();
        data.damage = damage;
        data.direction = (transform.position - col.transform.position).normalized;
        data.position = (transform.position);
        data.staggerIncrease = 0.3f;
        var hp = col.GetComponent<IDamagable>();
        
        hp.DealDamage(data);
    }
}
