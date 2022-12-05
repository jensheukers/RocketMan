using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RM_CharacterController : MonoBehaviour {

    private bool isMoving;

    private void Start() {
        isMoving = false;
    }

    /**
     * @brief Returns true if player is moving
     * @return bool
     */
    public bool IsMoving() {
        return isMoving;
    }
}
