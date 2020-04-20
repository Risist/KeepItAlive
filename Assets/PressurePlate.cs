using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Mechanism), typeof(SimpleAnimation))]
public class PressurePlate : MonoBehaviour
{

    Mechanism mecha;
    SimpleAnimation simpleAnim;

    public float resetTime;
    public bool blocked = false;
    void Start()
    {
        simpleAnim = GetComponent<SimpleAnimation>();
        mecha = GetComponent<Mechanism>();
    }
    void press()
    {
        if (!blocked)
        {
            blocked = true;
            mecha.trip();
            StartCoroutine(wait());
        }
    }
    IEnumerator wait()
    {
        simpleAnim.toAnimationFrame(1);
        yield return new WaitForSeconds(resetTime);
        simpleAnim.toAnimationFrame(0);
        blocked = false;
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        press();
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        press();
    }
}


