using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Transform target;
    public Transform player;
    public int testKeyCode;

    [Header("Free form")]
    public float freeDistance;
    public float maxDistance;
    public float correctionFactor;
    public DamageOnCollision[] freeFormDamage;

    [Header("Follow strict")]
    public float closeDistance = 0.5f;
    public float farDistance = 0.5f;
    [Range(0, 1)] public float strictLerpFactorClose = 0.1f;
    [Range(0, 1)] public float strictLerpFactorFar = 0.1f;
    [Space]
    [Header("Explosion")]
    public LayerMask requiredExplosionMask;
    public GameObject explosionPrefab;
    public Timer tExplosion;

    Rigidbody2D body;
    bool bFreeForm;


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
        {
            FreeFormUpdate();
        }

        foreach (var it in freeFormDamage)
            it.enabled = bFreeForm;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(target.position, maxDistance);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // only do fire in free form
        if (!bFreeForm)
            return;

        if (collision.contactCount == 0 || !tExplosion.IsReadyRestart())
            return;

        // ensure only specified masks will cause explosion
        if ((requiredExplosionMask.value & (1 << collision.gameObject.layer) ) == 0)
            return;

        Vector3 spawnPosition = collision.GetContact(0).point;
        Vector2 toOther = transform.position - player.position;
        Instantiate(explosionPrefab, spawnPosition, Quaternion.FromToRotation(Vector2.up, toOther));
    }
}
