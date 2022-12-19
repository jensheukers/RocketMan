using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RM_AICharacterController : RM_CharacterController {
    private Transform target;

    private Transform player;

    NavMeshAgent agent;

    [SerializeField]
    private float attackDistance = 20f;

    bool isChasingPlayer;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("RM_Player").transform;

        isChasingPlayer = false;
    }

    protected override void LateUpdate() {
        HandleMovement();
    }

    protected virtual void HandleMovement() {
    
        if (Vector3.Distance(player.position, transform.position) <= attackDistance) {
            //Chasing player state

            isChasingPlayer = true;
            target = player;
        }
        else {
            //Patrol state

            isChasingPlayer = false;
        }

        if (target) {
            if (Vector3.Distance(target.position, transform.position) > 1) {
                agent.SetDestination(target.position);
                HandleAnimations(new Vector2(0, 1));
            }
            else {
                HandleAnimations(new Vector2(0, 0));
            }
        }
    }

    void OnDrawGizmos() {
        if (!player) return;
        if (!isChasingPlayer) {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, player.position);
        }
        else {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, player.position);
        }
    }
}
