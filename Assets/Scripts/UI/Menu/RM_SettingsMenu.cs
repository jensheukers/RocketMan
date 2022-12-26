using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RM_SettingsMenu : MonoBehaviour { 
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            transform.gameObject.SetActive(false);
        }
    }
}
