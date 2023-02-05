using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Handles the healthbar
/// </summary>
public class RM_HealthBarHandle : MonoBehaviour {
    [SerializeField]
    private Slider slider; /** The slider reference*/

    private void Update() {
        RM_HealthComponent healthComponent = GetComponent<RM_HealthComponent>();
        if (healthComponent) {
            if (slider != null) slider.value = (float)healthComponent.GetHealth() / (float)healthComponent.GetMaxHealth();
        }
    }
}
