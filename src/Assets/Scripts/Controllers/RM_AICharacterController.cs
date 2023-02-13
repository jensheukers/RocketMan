using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

/// <summary>
/// AI_State Enum
/// </summary>
public enum RM_AiState {
    Patrolling,
    Chasing,
    Attacking
}

/// <summary>
/// Controller for AI in RocketMan
/// </summary>
public class RM_AICharacterController : RM_CharacterController {
    private Transform target; /** The target transform */

    private Transform player; /** The player transform*/

    NavMeshAgent agent;/**NavmeshAgent refernce*/

    [SerializeField]
    private float chaseDistance = 20f; /** Chase Distance*/

    [SerializeField]
    private float attackDistance = 2f;/** Attack Distance*/

    [SerializeField]
    private float attackInterval = 5f; /**Time between attacks*/

    [SerializeField]
    protected List<Transform> partrolPoints;/**All partrol points*/

    [SerializeField]
    protected UnityEvent<Transform> onAttack; /**OnAttack event*/

    [SerializeField]
    protected UnityEvent<Transform> onChase;/**OnChase event*/

    [SerializeField]
    protected UnityEvent<Transform> onPatrol;/**OnPatrol event*/

    [SerializeField]
    protected bool lookAtOnAttack = true;/**If true the player looks towards the target when attacking*/

    [SerializeField]
    private bool applyDamageOnAttack = false; /**If true damage will be applied on attack instantly if target has healthcomponent*/
    [SerializeField]
    private int applyDamageOnAttack_Damage = 20;

    RM_AiState state;/**OnAttack event*/

    private bool canAttack; /** canAttack boolean*/

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("RM_Player").transform;

        canAttack = true;
    }

    protected override void LateUpdate() {
        HanldeAIBehaviour();
    }

    /**
     * @brief Overwritten method (ai behaviour)
     */
    protected virtual void HanldeAIBehaviour() {
        if (player && Vector3.Distance(player.position, transform.position) <= chaseDistance) {
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
                if (target == player) {
                    OnAttack();
                }
                else {
                    target = null;
                }
            }
        }
        else {
            HandleAnimations(new Vector2(0, 0));
        }
    }

    /**
     * @brief Handles state changes and invokes events
     * @param RM_AiState state
     */
    private void ChangeState(RM_AiState state) {
        if (this.state == state) return;
        this.state = state;

        switch (state) {
            case RM_AiState.Patrolling:
                onPatrol.Invoke(null);
                break;
            case RM_AiState.Chasing:
                onChase.Invoke(target);
                break;
            case RM_AiState.Attacking:
                //Onattack gets called from within its method
                break;
            default:
                break;
        }
    }

    /**
     * @brief Sets the patrolling state
     */
    protected virtual void OnPatrolling() {
        ChangeState(RM_AiState.Patrolling);
        if (target == null || target == player) {
            //set new target

            if (partrolPoints.Count > 0) {
                target = partrolPoints[Random.Range(0, partrolPoints.Count)];
            }
        }
    }

    /**
     * @brief Sets the chasing state
     */
    protected virtual void OnChase() {
        ChangeState(RM_AiState.Chasing);

        //Chasing player state
        target = player;
        onChase.Invoke(target);
    }


    /**
     * @brief Sets the attacking state
     */
    protected virtual void OnAttack() {
        ChangeState(RM_AiState.Attacking);

        //Attack state
        agent.isStopped = true;

        if (lookAtOnAttack) {
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        }

        HandleAnimations(new Vector2(0, 0));

        //Handle attack
        if (canAttack) {
            if (GetComponent<Animator>()) {
                GetComponent<Animator>().SetTrigger("OnAttack");
            }

            onAttack.Invoke(target);
            canAttack = false;

            if (applyDamageOnAttack) {
                RM_HealthComponent healthComponent;
                if (healthComponent = target.GetComponent<RM_HealthComponent>()) {
                    healthComponent.Damage(applyDamageOnAttack_Damage);
                }
            }

            StartCoroutine(ResetAttack());
        }
    }

    //private methods

    /**
     * @brief Resets the attack
     */
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

        if (state == RM_AiState.Patrolling && target) {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, target.position);
        }
    }
}