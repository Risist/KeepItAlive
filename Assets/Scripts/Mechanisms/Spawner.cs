using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float spawnRate;
    public bool isSpawning;
    public Spawnable spawnable;
   /// public Vector3 spawnPoint;
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
        spawnable.spawn(transform.position, velocity * transform.up,transform.up);
    }
}
