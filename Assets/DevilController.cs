using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilController : MonoBehaviour
{
    GameObject player;
    new Animator animator;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();

    }

    void LookAtPlayer(float lerpScale)
    {
        if (!player)
        {
            Debug.LogWarning("No player set up");
            return;
        }
        Vector2 toPlayer = transform.position - player.transform.position;
        float desiredAngle = Vector2.SignedAngle(Vector2.up, toPlayer);

        float currentAngle = transform.rotation.eulerAngles.z;
        float finalAngle = Mathf.LerpAngle(currentAngle, desiredAngle, lerpScale);

        transform.rotation = Quaternion.Euler(0, 0, finalAngle);
    }

    void RandomBlink(float chance)
    {
        if(Random.value < chance)
            animator.SetTrigger("CanBlink");
    }

    void MoveTo(Vector2 destination, float movementSpeed, float rotationSpeed)
    {

    }

    void Update()
    {
        LookAtPlayer(0.1f);
        RandomBlink(0.00075f);
    }
}
