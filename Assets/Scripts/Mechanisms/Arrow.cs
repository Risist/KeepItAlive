using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Spawnable
{
    public bool isPiercing;

    public override void SetupLiftime()
    {
        DefaultLifetime();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(!isPiercing) Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collision2D collision){ 
    
    }
}
