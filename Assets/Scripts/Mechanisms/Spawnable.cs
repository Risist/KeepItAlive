﻿using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Spawnable : MonoBehaviour {

    public float lifetime = 1.0f;

    public Spawnable spawn(Vector2 position, Vector2 velocity, Vector2 direction)
    {
        GameObject obj = Instantiate(gameObject, position,
            Quaternion.AngleAxis(Vector2.Angle(Vector2.up, direction), -Vector3.forward));
        Spawnable spwn = obj.GetComponent<Spawnable>();
        Rigidbody2D rigid = spwn.GetComponent<Rigidbody2D>();
        rigid.velocity = velocity;
        spwn.SetupLiftime();
        return spwn;
    }

    public void SetupLiftime() {
        DefaultLifetime();
    }
    protected void DefaultLifetime() {
        Destroy(gameObject, lifetime);
    }


}


