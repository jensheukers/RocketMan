using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum RM_AiState {
    Patrolling,
    Chasing,
    Attacking
}

public class RM_AICharacterController : RM_CharacterController {
    private Transform target;

    private Transform player;

    NavMeshAgent agent;

    [SerializeField]
    private float chaseDistance = 20f;

    [SerializeField]
    private float attackDistance = 2f;

    [SerializeField]
    private float attackInterval = 5f;

    RM_AiState state;

    private bool canAttack;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("RM_Player").transform;

        canAttack = true;
    }

    protected override void LateUpdate() {
        HandleMovement();
    }

    protected virtual void HandleMovement() {
    
        if (Vector3.Distance(player.position, transform.position) <= chaseDistance) {
            OnChase();
        }
        else {
            OnPatrolling();
        }

        if (target) {
            //Move towards target
            if (Vector3.Distance(target.position, transform.position) > attackDistance) {
                agent.isStopped = false;
                agent.SetDestination(target.position);
                HandleAnimations(new Vector2(0, 1));
            }
            else {
                OnAttack();
            }
        }
        else {
            HandleAnimations(new Vector2(0, 0));
        }
    }

    protected virtual void OnPatrolling() {
        state = RM_AiState.Patrolling;
        target = null;
    }

    protected virtual void OnChase() {
        state = RM_AiState.Chasing;

        //Chasing player state
        target = player;
    }

    protected virtual void OnAttack() {
        state = RM_AiState.Attacking;

        //Attack state
        agent.isStopped = true;

        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        HandleAnimations(new Vector2(0, 0));

        //Handle attack
        if (canAttack) {
            GetComponent<Animator>().SetTrigger("OnAttack");
            canAttack = false;

            StartCoroutine(ResetAttack());
        }
    }

    //private methods
    private IEnumerator ResetAttack() {
        yield return new WaitForSeconds(attackInterval);
        canAttack = true;
    }

    void OnDrawGizmos() {
        if (!player) return;
        if (state != RM_AiState.Chasing) {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, player.position);
        }
        else {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, player.position);
        }
    }
}
