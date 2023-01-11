using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// RN_CharacterController is the main controller class for controlling objects in RocketMan
/// </summary>
public class RM_CharacterController : MonoBehaviour {

    protected bool isMoving; /** True if character is moving*/
    protected float distToGround;

    [SerializeField]
    private float horizontalSpeed = 5f; /** The horizontal movement speed*/

    [SerializeField]
    private float verticalSpeed = 5f; /** The vertical movement speed*/

    [SerializeField]
    private float rollIncrement = 1f;

    [SerializeField]
    private RM_Jetpack jetPack; /** Jetpack reference */

    void Start() {
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    protected virtual void LateUpdate() {
        isMoving = false;
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        bool roll = false;
        if (Input.GetKeyDown(KeyCode.C)) { roll = true; }

        if (input.x != 0 || input.y != 0) {
            HandleMovement(input, roll);
        }

        if (Input.GetKey(KeyCode.Space) && jetPack) {
            jetPack.Boost(this.gameObject);
        }

        HandleAnimations(input, roll);
    }

    /*
     * Handles Movement
     * @param Vector2 input
     */
    protected virtual void HandleMovement(Vector2 input, bool roll = false) {
        Vector3 targetPos;

        if (roll) input.x += rollIncrement;

        targetPos = (transform.forward * input.y) * horizontalSpeed;
        targetPos += (transform.right * input.x) * verticalSpeed;

        transform.position = Vector3.Lerp(transform.position, transform.position + targetPos, Time.deltaTime);

        isMoving = true;
    }

    /*
     * Hanldes animations
     * @param Vector2 input
     */
    protected virtual void HandleAnimations(Vector2 input, bool roll = false) {
        //Set movement blend tree variables
        Animator animator = GetComponent<Animator>();
        if (!animator) return;


        animator.SetFloat("Horizontal", input.x);
        animator.SetFloat("Vertical", input.y);

        if (roll) animator.SetTrigger("Roll");
    }

    /**
     * @brief Returns true if character is moving
     * @return bool
     */
    public bool IsMoving() {
        return isMoving;
    }

    /**
     * @brief Returns true if character is grounded
     * @return bool
     */
    public bool IsGrounded() {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);

    }


    /*
     * @brief returns the jetpack instance
     */
    public RM_Jetpack GetJetpack() {
        return jetPack;
    }
}
