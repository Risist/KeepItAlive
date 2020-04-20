using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Mechanism))]
public class OnCollisionTrip : MonoBehaviour
{

    Mechanism mecha;
    public float resetTime;
    public bool blocked = false;
    void Start() {
        mecha = GetComponent<Mechanism>();
    }


    void OnTriggerEnter2D(Collider2D c) {
        if (!blocked)
        {
            blocked = true;
            mecha.trip();
        }
    }

    void OnCollisionEnter2D(Collision2D c) {
        mecha.trip();
    }


}
