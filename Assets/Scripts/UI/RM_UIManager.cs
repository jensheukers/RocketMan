using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RM_UIManager : MonoBehaviour {
    [SerializeField]
    private Slider healthBarSlider;

    [SerializeField]
    private Slider jetpackFuelSlider;

    private void Update() {
        RM_HealthComponent healthComponent;
        if (healthComponent = GetComponent<RM_HealthComponent>()) {
            healthBarSlider.value = healthComponent.GetHealth() / healthComponent.GetMaxHealth();
        }

        RM_Jetpack jetpack;
        if (jetpack = GetComponent<RM_CharacterController>().GetJetpack()) {
            jetpackFuelSlider.value = jetpack.GetFuel() / jetpack.GetMaxFuel();
        }
    }
}
