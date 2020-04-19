using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilBlackboard
{
    public Vector2 destination;
    public GameObject player;
    public SquadBlackboard squadBlackboard;

    public bool bCommandAttack
    {
        get { return squadBlackboard != null ? squadBlackboard.bCommandAttack : false; }
    }
}

public class DevilController : MonoBehaviour
{
    public bool canMove = true;
    public bool canRotate = true;
    public bool canPerformAction = true;

    public DevilBlackboard blackboard = new DevilBlackboard();
    Animator animator;
    new Rigidbody2D rigidbody;
    
    void Start()
    {
        blackboard.player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();

        InitStates();
    }

    #region States

    void InitStates()
    {
        Timer tChangeState = new Timer();

        var stateIdle = stateMachine.AddNewStateAsCurrent()
            .AddOnBegin(() => tChangeState.RestartRandom(0.25f, 0.5f) )
            .AddOnUpdate( () =>
            {
                RandomBlink(0.00075f);
                RandomScare(0.00125f);
                
            })

            .SetReturnState(tChangeState.IsReady)
            .SetGetNextState(stateMachine.GetNextStateByUtility)
            ;

        var stateRandomWalk = stateMachine.AddNewState()
            .AddOnBegin(() => tChangeState.RestartRandom(0.75f, 1.25f))
            .AddOnBegin(() => blackboard.destination = GetRandomPosition(5))
            .AddOnUpdate(() =>
            {
                MoveTo(blackboard.destination, 10, 1);
                RotateTo(blackboard.destination, 0.1f);

                Avoid(5, 3);
                Avoid(4, 5);
                Avoid(3, 6);
            })

            .SetReturnState(tChangeState.IsReady)
            .SetGetNextState(stateMachine.GetNextStateByUtility)
            ;

        var stateObserve = stateMachine.AddNewState()
            .AddOnBegin(() => tChangeState.RestartRandom(0.75f, 1.5f))
            .AddOnUpdate(() =>
            {
                Vector2 playerPosition = blackboard.player.transform.position;

                RandomBlink(0.00075f);
                if (!IsCloseTo(playerPosition, 12))
                    RandomScare(0.0025f);

                RotateTo(playerPosition, 0.1f);

                if (IsCloseTo(playerPosition, 5))
                    Hit(0.01f);
            })

            .SetReturnState(tChangeState.IsReady)
            .SetGetNextState(stateMachine.GetNextStateByUtility)

            .SetCanEnter(() => blackboard.player != null)
            ;

        var stateAttack = stateMachine.AddNewState()
            .AddOnBegin(() => tChangeState.RestartRandom(1.25f, 2.5f))
            .AddOnUpdate(() => {
                Vector2 playerPosition = blackboard.player.transform.position;

                RandomBlink(0.00075f);

                RotateTo(playerPosition, 0.1f);
                MoveTo(playerPosition, 10, 4);

                Avoid(5, 4);
                Avoid(4, 6);
                Avoid(3, 8);

                if (IsCloseTo(playerPosition, 7))
                    Hit(0.01f);
                else
                {
                    float chance = IsCloseTo(playerPosition, 12) ? 0.02f : 0.05f; 
                    RandomDash(chance);
                }
            })

            .SetReturnState(tChangeState.IsReady)
            .SetGetNextState(stateMachine.GetNextStateByUtility)

            .SetCanEnter(() => blackboard.player != null && blackboard.bCommandAttack)
            .SetUtility(() =>  850.0f )
            ;

        var stateAwayFromPlayer = stateMachine.AddNewState()
            .AddOnBegin(() => tChangeState.RestartRandom(0.75f, 1.5f))
            .AddOnBegin(() => blackboard.destination = GetAwayPosition(blackboard.player.transform.position, 15))
            .AddOnUpdate(() =>
            {
                MoveTo(blackboard.destination, 35, 1);
                RotateTo(blackboard.destination, 0.1f);

                RandomDash(0.0065f);

                Avoid(5, 3);
                Avoid(4, 5);
                Avoid(3, 6);
            })

            .SetCanEnter(() => IsCloseTo(blackboard.player.transform.position, 17) && !blackboard.bCommandAttack )
            .SetReturnState(tChangeState.IsReady)
            .SetGetNextState(stateMachine.GetNextStateByUtility)
            .SetUtility( () => 40 );
            ;

        var stateStayCloseToPlayer = stateMachine.AddNewState()
            .AddOnBegin(() => tChangeState.RestartRandom(0.75f, 1.5f))
            .AddOnBegin(() => blackboard.destination = GetAwayPosition(blackboard.player.transform.position, 15))
            .AddOnUpdate(() =>
            {
                Vector2 playerPosition = blackboard.player.transform.position;
                MoveTo(playerPosition, 10, 20);
                RotateTo(playerPosition, 0.1f);

                Avoid(5, 3);
                Avoid(4, 5);
                Avoid(3, 6);
            })

            .SetCanEnter(() => !IsCloseTo(blackboard.player.transform.position, 20) && !blackboard.bCommandAttack)
            .SetReturnState(tChangeState.IsReady)
            .SetGetNextState(stateMachine.GetNextStateByUtility)
            .SetUtility(() => 0.5f);
        ;

        // need for a way for a controlled transition force 

    }
    readonly StateMachine stateMachine = new StateMachine();

    #endregion States

    #region Fundamental Actions
    /// info
    bool IsCloseTo(Vector2 destination, float closeDistance)
    {
        Vector2 toDestination = destination - (Vector2)transform.position;

        return toDestination.sqrMagnitude < closeDistance * closeDistance;
    }
    Vector2 GetRandomPosition(float maxDestinationOffset)
    {
        return (Vector2)transform.position + Random.insideUnitCircle * maxDestinationOffset;
    }
    Vector2 GetAwayPosition(Vector2 from, float distance)
    {
        Vector2 toDestination = from - (Vector2)transform.position;
        return (Vector2)transform.position - toDestination.normalized*distance;
    }

    /// Actions
    void RandomBlink(float chance)
    {
        if (canPerformAction && Random.value < chance)
            animator.SetTrigger("CanBlink");
    }
    void RandomScare(float chance)
    {
        if (canPerformAction && Random.value < chance)
            animator.SetTrigger("Scare");
    }
    void RandomDash(float chance)
    {
        if (canPerformAction && Random.value < chance)
            animator.SetTrigger("Dash");
    }
    void Hit(float chance)
    {
        if (canPerformAction && Random.value < chance)
            animator.SetTrigger("Hit");
    }

    /// Movement
    void MoveTo(Vector2 destination, float movementSpeed, float closeDistance)
    {
        if (!canMove)
            return;

        Vector2 toDestination = destination - (Vector2)transform.position;
        
        if (toDestination.sqrMagnitude < closeDistance * closeDistance)
            return;

        rigidbody.AddForce(toDestination.normalized * movementSpeed);
    }
    void RotateTo(Vector2 destination, float rotationSpeed)
    {
        if (!canRotate)
            return;
        Vector2 toPlayer = (Vector2)transform.position - destination;
        float desiredAngle = Vector2.SignedAngle(Vector2.up, toPlayer);

        float currentAngle = transform.rotation.eulerAngles.z;
        float finalAngle = Mathf.LerpAngle(currentAngle, desiredAngle, rotationSpeed);

        transform.rotation = Quaternion.Euler(0, 0, finalAngle);
    }
    void Avoid(float radius, float awayForce)
    {
        if (!canMove)
            return;

        var coll = Physics2D.OverlapCircle(transform.position, radius, 1 << LayerMask.NameToLayer("Entity") );
        if (!coll)
            return;

        Vector2 toDestination = (Vector2)coll.transform.position - (Vector2)transform.position;
        rigidbody.AddForce(-toDestination.normalized * awayForce);
    }
    #endregion Fundamental Actions

    void Update()
    {
        stateMachine.UpdateStates();
    }
}
