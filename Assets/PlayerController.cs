using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    void Start()
    {
        var animator = GetComponentInParent<Animator>();
        var health = GetComponentInParent<HealthController>();
        health.onStaggerCallback += (data) => { animator.SetTrigger("Stagger"); };
    }
}
