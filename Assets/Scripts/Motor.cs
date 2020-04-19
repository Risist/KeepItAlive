using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor : MonoBehaviour
{
    public float force;
    Rigidbody2D body;

    private void Start()
    {
        body = GetComponentInParent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        body.AddForce(transform.up * force);
    }
}
