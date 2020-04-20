using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SquadBlackboard
{
    public bool bCommandAttack;
}

public class SquadController : MonoBehaviour
{
    public GameObject squadMemberPrefab;
    public int squadCount;
    public float spawnRadius;
    public float playerDistanceToSpawn = float.PositiveInfinity;

    [Header("States")]
    public Timer tOffense;

    public float ofenseChance = 0.000125f;

    List<GameObject> squadMembers = new List<GameObject>();
    public SquadBlackboard blackboard = new SquadBlackboard();

    Transform player;
    bool spawned;

    public void SpawnSquadMember(float radius)
    {
        Vector2 position = (Vector2)transform.position + Random.insideUnitCircle * radius;
        
        var obj = Instantiate(squadMemberPrefab, position, transform.rotation);
        squadMembers.Add(obj);
        var controller = obj.GetComponent<DevilController>();
        controller.blackboard.squadBlackboard = blackboard;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void FixedUpdate()
    {
        if (!spawned && Vector2.Distance(player.position, transform.position) < playerDistanceToSpawn)
        {
            spawned = true;
            for (int i = 0; i < squadCount; ++i)
                SpawnSquadMember(spawnRadius);
        }

        if (blackboard.bCommandAttack)
        {
            if (tOffense.IsReady())
            {
                blackboard.bCommandAttack = false;
            }
        }else if (Random.value < ofenseChance)
        {
            tOffense.Restart();
            blackboard.bCommandAttack = true;
        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawCube(Vector3.zero, Vector3.one); 
        Gizmos.DrawCube(-Vector3.up, new Vector3(0.25f, 1.0f, 0.25f));

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red * 0.85f;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireSphere(Vector3.zero, playerDistanceToSpawn);
    }
}
