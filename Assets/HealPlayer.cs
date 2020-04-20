using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPlayer : MonoBehaviour
{
    public float healValue;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var hp = collision.gameObject.GetComponent<HealthController>();
            if (hp)
            {
                hp.DealDamage(-healValue * Time.fixedDeltaTime, 0);
            }
        }
    }
}
