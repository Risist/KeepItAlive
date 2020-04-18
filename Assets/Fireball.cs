using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Transform target;
    public bool bFreeForm;
    public int testKeyCode;
    [Header("Free form")]
    public float freeDistance;
    public float maxDistance;
    public float correctionFactor;

    [Header("Follow strict")]
    public float closeDistance = 0.5f;
    public float farDistance = 0.5f;
    [Range(0, 1)] public float strictLerpFactorClose = 0.1f;
    [Range(0, 1)] public float strictLerpFactorFar = 0.1f;
    
    Rigidbody2D body;


    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        transform.parent = null;
    }

    void FreeFormUpdate()
    {
        Vector3 toTarget = target.position - transform.position;
        toTarget.z = 0;
        if (toTarget.sqrMagnitude < freeDistance * freeDistance)
        {
            body.velocity *= 0.85f;
            return;
        }
        if (toTarget.sqrMagnitude > maxDistance * maxDistance)
        {
            body.velocity *= 0.9f;//Vector2.zero;
            //body.position = target.position - toTarget.normalized * maxDistance;

            toTarget.Normalize();
            Vector2 force = new Vector2(toTarget.x, toTarget.y) * correctionFactor * 5.25f;
            body.AddForce(force);
            return;
        }


        {
            toTarget.Normalize();
            Vector2 force = new Vector2(toTarget.x, toTarget.y) * correctionFactor;// + Random.insideUnitCircle*1.2f*correctionFactor;
            body.AddForce(force);
        }
    }

    bool StrictUpdate()
    {
        float dist = Vector2.Distance(transform.position, target.position);
        bool bIsClose = dist < closeDistance;
        bool bIsFar = dist < farDistance;
        if (bIsFar)
        {
            float lerpFactor = bIsClose ? strictLerpFactorClose : 
                Mathf.Lerp(strictLerpFactorClose,0, dist/(farDistance));

            if (bIsClose)
                body.velocity = Vector2.zero;

            /*Debug.Log(
                (dist) / (farDistance) + " |||| " +
                dist + " |||| " + (closeDistance - dist) + " |||| " + (farDistance- closeDistance) );*/
            lerpFactor = Mathf.Clamp01(lerpFactor);
            //lerpFactor = Mathf.Min(lerpFactor, strictLerpFactorFar);
            //lerpFactor = strictLerpFactorClose;
            transform.position = Vector3.Lerp(transform.position, target.position, lerpFactor);
        }
        return bIsClose;
    }
    void FixedUpdate()
    {

        bFreeForm = Input.GetMouseButton(testKeyCode);
        if (bFreeForm || !StrictUpdate())
            FreeFormUpdate();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(target.position, maxDistance);
    }


}
