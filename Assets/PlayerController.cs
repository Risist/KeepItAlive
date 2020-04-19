using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool canPerformAction = true;

    public bool canUseTorchLeft
    {
        get { return canPerformAction && _useLeftTorch; }
    }
    bool _useLeftTorch;

    public bool canUseTorchRight
    {
        get{ return canPerformAction && _useRightTorch; }
    }
    bool _useRightTorch;

    public Timer tDashCd;

    Animator animator;
    void Start()
    {
        animator = GetComponentInParent<Animator>();
        var health = GetComponentInParent<HealthController>();
        health.onStaggerCallback += (data) => { animator.SetTrigger("Stagger"); };
    }

    private void Update()
    {
        if (canPerformAction && Input.GetButton("Fire3") && tDashCd.IsReadyRestart())
        {
            animator.SetTrigger("Dash");
        }

        if (Input.GetButtonDown("Fire1"))
            _useLeftTorch = !_useLeftTorch;


        if (Input.GetButtonDown("Fire2"))
            _useRightTorch = !_useRightTorch;
    }


}
