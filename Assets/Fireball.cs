using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Transform target;
    public float freeDistance;
    public float maxDistance;
    public float correctionFactor;
    Rigidbody2D body;


    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        transform.parent = null;
    }
    void FixedUpdate()
    {
        Vector3 toTarget = target.position - transform.position;
        toTarget.z = 0;
        if (toTarget.sqrMagnitude < freeDistance * freeDistance)
        {
            body.velocity *= 0.95f;
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(target.position, maxDistance);
    }


}
