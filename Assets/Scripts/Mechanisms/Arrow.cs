using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public bool isPiercing;

    void OnCollisionEnter2D(Collision2D collision) {
        //if(!isPiercing && collision.gameObject.GetComponent<HealthController>() != null) Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D collision){
        //if (!isPiercing && collision.gameObject.GetComponent<HealthController>() != null ) Destroy(gameObject);
    }
}
