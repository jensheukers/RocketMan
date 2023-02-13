using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// RN_CharacterController is the main controller class for controlling objects in RocketMan
/// </summary>
public class RM_CharacterController : MonoBehaviour {

    protected bool isMoving; /** True if character is moving*/
    protected bool isGrounded;
    protected bool canRoll;

    [SerializeField]
    private float horizontalSpeed = 5f; /** The horizontal movement speed*/

    [SerializeField]
    private float verticalSpeed = 5f; /** The vertical movement speed*/

    [SerializeField]
    private float rollIncrement = 1f; /**The increment in position when rolling*/

    [SerializeField]
    private float rollCooldown = 1f; /** The cooldown for rolling*/

    [SerializeField]
    private RM_Jetpack jetPack; /** Jetpack reference */

    protected AudioSource _audioSource; /**Reference to audio source*/

    private void Start() {
        canRoll = true;

        _audioSource = GetComponent<AudioSource>();
    }

    //Input should eventually be called from a different class
    protected virtual void LateUpdate() {
        if (Time.timeScale == 0) return;

        isMoving = false;
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        //Handle rolling input
        bool roll = false;
        if (Input.GetButtonDown("Fire3") && IsGrounded() && canRoll && input.y > 0) { 
            roll = true;
            canRoll = false;

            StartCoroutine("ResetCanRoll");
        }

        if (input.x != 0 || input.y != 0) {
            HandleMovement(input, roll);

            if (_audioSource && !_audioSource.isPlaying && isGrounded) _audioSource.Play();
        }
        else {
            if (_audioSource && _audioSource.isPlaying) {
                _audioSource.Stop();
            }
        }

        if (_audioSource && _audioSource.isPlaying && !isGrounded) _audioSource.Stop();

        if (Input.GetButton("Jump") && jetPack) {
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
     * Resets canRoll bool
     */
    private IEnumerator ResetCanRoll() {
        yield return new WaitForSeconds(rollCooldown);
        canRoll = true;
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
        return isGrounded;
    }


    /*
     * @brief returns the jetpack instance
     */
    public RM_Jetpack GetJetpack() {
        return jetPack;
    }

    public void OnCollisionEnter(Collision collision) {
        if (collision.transform.tag == "RM_Ground") {
            isGrounded = true;
        }
;    }

    public void OnCollisionExit(Collision collision) {
        if (collision.transform.tag == "RM_Ground") {
            isGrounded = false;
        }
    }

}
