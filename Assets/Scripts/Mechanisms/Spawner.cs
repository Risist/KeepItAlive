using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float spawnRate;
    public bool isSpawning;
    public GameObject prefab;
    public float lifetime;

    public float velocity;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawn());
    }

    IEnumerator spawn() {
        while (true) {
            if (isSpawning) {
                Spawn();
            }
            yield return new WaitForSeconds(spawnRate);
        }

    }
    public void Spawn() { // for trigger once, this is ignoring checking isSpawning

        var obj = Instantiate(prefab, transform.position, transform.rotation);

        Destroy(obj, lifetime);

        Rigidbody2D rigid = obj.GetComponent<Rigidbody2D>();
        rigid.velocity = velocity * transform.up;
    }
}
