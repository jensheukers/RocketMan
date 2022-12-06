using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RM_CharacterController : MonoBehaviour {

    private bool isMoving; /** True if player is moving*/

    [SerializeField]
    private float horizontalSpeed = 5f;

    [SerializeField]
    private float verticalSpeed = 5f;

    private void Update() {
        isMoving = false;
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (input.x != 0 || input.y != 0) {
            //transform.Translate(new Vector3((input.x * horizontalSpeed) * Time.deltaTime, 0, (input.y * verticalSpeed) * Time.deltaTime));


            transform.position += (transform.forward * input.y) * horizontalSpeed * Time.deltaTime;
            transform.position += (transform.right * input.x) * verticalSpeed * Time.deltaTime;

            isMoving = true;
        }
    }

    /**
     * @brief Returns true if player is moving
     * @return bool
     */
    public bool IsMoving() {
        return isMoving;
    }
}
