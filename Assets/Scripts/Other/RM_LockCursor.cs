using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RM_LockCursor : MonoBehaviour {
    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
