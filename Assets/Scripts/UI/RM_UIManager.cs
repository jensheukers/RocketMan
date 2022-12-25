using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RM_UIManager : MonoBehaviour {
    [SerializeField]
    private Slider healthBarSlider;

    [SerializeField]
    private Slider jetpackFuelSlider;

    [SerializeField]
    private TMP_Text weaponName;

    [SerializeField]
    private TMP_Text ammoAmount;

    private void Update() {
        RM_HealthComponent healthComponent;
        if (healthComponent = GetComponent<RM_HealthComponent>()) {
            healthBarSlider.value = healthComponent.GetHealth() / healthComponent.GetMaxHealth();
        }

        RM_Jetpack jetpack;
        if (jetpack = GetComponent<RM_CharacterController>().GetJetpack()) {
            jetpackFuelSlider.value = jetpack.GetFuel() / jetpack.GetMaxFuel();
        }

        RM_WeaponManager weaponManager;
        if (weaponManager = GetComponent<RM_WeaponManager>()) {
            if (weaponManager.GetCurrentWeapon()) weaponName.text = weaponManager.GetCurrentWeaponData().name;
            ammoAmount.text = weaponManager.GetCurrentWeapon().GetAmmo().ToString();
        }
    }
}
