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

    [Header("States")]
    public Timer tOffense;

    public float ofenseChance = 0.000125f;

    List<GameObject> squadMembers = new List<GameObject>();
    public SquadBlackboard blackboard = new SquadBlackboard();

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
        for (int i = 0; i < squadCount; ++i)
            SpawnSquadMember(spawnRadius);
    }
    private void Update()
    {
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
            Debug.Log("offense");
        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawCube(Vector3.zero, Vector3.one); 
        Gizmos.DrawCube(-Vector3.up, new Vector3(0.25f, 1.0f, 0.25f));
    }
}
