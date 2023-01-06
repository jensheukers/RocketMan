using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Handles UI Canvas Object that can be added as a child to the player object
/// </summary>
public class RM_UIManager : MonoBehaviour {
    [SerializeField]
    private Slider healthBarSlider; /*** Reference to the healthbar slider*/

    [SerializeField]
    private Slider jetpackFuelSlider; /*** Reference to the jet pack fuel slider*/

    [SerializeField]
    private TMP_Text weaponName; /*** Reference to the weaponname text*/

    [SerializeField]
    private TMP_Text ammoAmount; /*** Reference to the ammo text*/

    [SerializeField]
    private TMP_Text questName; /*** Reference to the quest name text */

    [SerializeField]
    private TMP_Text taskName; /*** Reference to the task name text */

    [SerializeField]
    private TMP_Text taskDescription; /*** Reference to the task description text */

    //TEMPORARY
    [SerializeField]
    private RM_MissionSO tempMainMissionData;
    //!TEMPORARY

    private void Update() {
        //TEMPORARY
        if (Input.GetKeyDown(KeyCode.P)) RM_GameState.GetCurrentMission().StopMission();

        //!TEMPORARy

        RM_HealthComponent healthComponent;
        if (healthComponent = GetComponent<RM_HealthComponent>()) {
            healthBarSlider.value = (float)healthComponent.GetHealth() / (float)healthComponent.GetMaxHealth();
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

        if (RM_GameState.InstanceExists() && RM_GameState.GetCurrentMission().MissionData().GetCurrentQuest()) {
            questName.text = RM_GameState.GetCurrentMission().MissionData().GetCurrentQuest().GetQuestName();
            taskName.text = RM_GameState.GetCurrentMission().MissionData().GetCurrentQuest().GetCurrentTask().GetTaskName();
            taskDescription.text = RM_GameState.GetCurrentMission().MissionData().GetCurrentQuest().GetCurrentTask().GetTaskDescription();
        }
    }
}
