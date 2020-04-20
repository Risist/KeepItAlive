using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyController : MonoBehaviour
{
    GameObject player;
    Animator animator;
    new Rigidbody2D rigidbody;
    private DialogueController dialogue;

    void RotateTo(Vector2 destination, float rotationSpeed)
    {
        Vector2 toPlayer = (Vector2)transform.position - destination;
        float desiredAngle = Vector2.SignedAngle(Vector2.up, toPlayer);

        float currentAngle = transform.rotation.eulerAngles.z;
        float finalAngle = Mathf.LerpAngle(currentAngle, desiredAngle, rotationSpeed);

        transform.rotation = Quaternion.Euler(0, 0, finalAngle);
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        dialogue = GetComponentInChildren<DialogueController>();
        dialogue.player = player;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RotateTo(player.transform.position, 0.1f);
    }
}
