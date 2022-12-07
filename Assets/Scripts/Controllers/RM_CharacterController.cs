using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// RN_CharacterController is the main controller class for controlling objects in RocketMan
/// </summary>
public class RM_CharacterController : MonoBehaviour {

    private bool isMoving; /** True if player is moving*/

    [SerializeField]
    private float horizontalSpeed = 5f; /** The horizontal movement speed*/

    [SerializeField]
    private float verticalSpeed = 5f; /** The vertical movement speed*/

    private void Update() {
        isMoving = false;
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (input.x != 0 || input.y != 0) {
            HandleMovement(input);
        }

        HandleAnimations(input);
    }

    /*
     * Handles Movement
     * @param Vector2 input
     */
    protected virtual void HandleMovement(Vector2 input) {
        transform.position += (transform.forward * input.y) * horizontalSpeed * Time.deltaTime;
        transform.position += (transform.right * input.x) * verticalSpeed * Time.deltaTime;

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
}
