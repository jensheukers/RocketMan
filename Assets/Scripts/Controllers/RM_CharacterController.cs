using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// RN_CharacterController is the main controller class for controlling objects in RocketMan
/// </summary>
public class RM_CharacterController : MonoBehaviour {

    protected bool isMoving; /** True if player is moving*/

    [SerializeField]
    private float horizontalSpeed = 5f; /** The horizontal movement speed*/

    [SerializeField]
    private float verticalSpeed = 5f; /** The vertical movement speed*/

    [SerializeField]
    private RM_Jetpack jetPack; /** Jetpack reference */

    protected virtual void LateUpdate() {
        isMoving = false;
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (input.x != 0 || input.y != 0) {
            HandleMovement(input);
        }

        if (Input.GetKey(KeyCode.Space) && jetPack) {
            jetPack.Boost(this.gameObject);
        }

        HandleAnimations(input);
    }

    /*
     * Handles Movement
     * @param Vector2 input
     */
    protected virtual void HandleMovement(Vector2 input) {
        Vector3 targetPos;

        targetPos = (transform.forward * input.y) * horizontalSpeed;
        targetPos += (transform.right * input.x) * verticalSpeed;

        transform.position = Vector3.Lerp(transform.position, transform.position + targetPos, Time.deltaTime);

        isMoving = true;
    }

    /*
     * Hanldes animations
     * @param Vector2 input
     */
    protected virtual void HandleAnimations(Vector2 input) {
        //Set movement blend tree variables
        Animator animator = GetComponent<Animator>();
        if (!animator) return;


        animator.SetFloat("Horizontal", input.x);
        animator.SetFloat("Vertical", input.y);
    }

    /**
     * @brief Returns true if player is moving
     * @return bool
     */
    public bool IsMoving() {
        return isMoving;
    }

    /*
     * @brief returns the jetpack instance
     */
    public RM_Jetpack GetJetpack() {
        return jetPack;
    }
}
