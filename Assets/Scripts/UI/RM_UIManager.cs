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
    private Canvas canvasObject; /** The canvas object*/
    private bool canvasActive; /**Canvas active boolean*/

    [SerializeField]
    private Slider healthBarSlider; /*** Reference to the healthbar slider*/

    [SerializeField]
    private Slider jetpackFuelSlider; /*** Reference to the jet pack fuel slider*/

    [SerializeField]
    private TMP_Text weaponName; /*** Reference to the weaponname text*/

    [SerializeField]
    private Image weaponIcon; /** Reference to the weapon icon image*/

    [SerializeField]
    private TMP_Text ammoAmount; /*** Reference to the ammo text*/

    [SerializeField]
    private TMP_Text questName; /*** Reference to the quest name text */

    [SerializeField]
    private TMP_Text taskName; /*** Reference to the task name text */

    [SerializeField]
    private TMP_Text taskDescription; /*** Reference to the task description text */

    [SerializeField]
    private GameObject escapeMenu; /** Reference to the escape menu */

    [SerializeField]
    private GameObject notificationObject;

    [SerializeField]
    private TMP_Text notificationText;

    [SerializeField]
    private GameObject headupNotifictionObject;

    [SerializeField]
    private TMP_Text headupNotifictionText;

    private bool escapeMenuActive;

    private void Start() {
        EnableUI();
        escapeMenu.SetActive(false);

        HideNotification();
        HideHeadupNotification(0);
    }

    private void Update() {
        if (!canvasActive) return;
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
            if (weaponManager.GetCurrentWeapon()) {

                weaponName.text = weaponManager.GetCurrentWeaponData().weaponName;
                ammoAmount.text = weaponManager.GetCurrentWeapon().GetAmmo().ToString();
                weaponIcon.sprite = weaponManager.GetCurrentWeaponData().icon;
            }
        }

        if (RM_GameState.InstanceExists() && RM_GameState.GetCurrentMission().MissionData().GetCurrentQuest()) {
            questName.text = RM_GameState.GetCurrentMission().MissionData().GetCurrentQuest().GetQuestName();
            taskName.text = RM_GameState.GetCurrentMission().MissionData().GetCurrentQuest().GetCurrentTask().GetTaskName();
            taskDescription.text = RM_GameState.GetCurrentMission().MissionData().GetCurrentQuest().GetCurrentTask().GetTaskDescription();
        }

        if (Input.GetButtonDown("Escape")) {
            if (!escapeMenuActive) {
                escapeMenuActive = true;
                escapeMenu.SetActive(true);

                //Unlock cursor
                Cursor.lockState = CursorLockMode.None;

                //Set Timescale
                Time.timeScale = 0;
            }
            else {
                escapeMenuActive = false;
                escapeMenu.SetActive(false);

                //Lock Cursor again
                Cursor.lockState = CursorLockMode.Locked;

                //Set Timescale back
                Time.timeScale = 1;
            }
        }

        //Look for input when player is in escape menu
        if (escapeMenuActive) {
            if (Input.GetButtonDown("Submit")) {
                RM_GameState.ReturnToMainMenu();
            }
        }
    }

    public bool EscapeMenuActive() {
        return escapeMenuActive;
    }

    public bool notificationActive() {
        return notificationObject.activeSelf;
    }

    public void ShowNotification() {
        notificationObject.SetActive(true);
    }

    public void ShowNotificationTime(int time) {
        ShowNotification();
        StartCoroutine("HideNotification", time);
    }

    public void HideNotification() {
        notificationObject.SetActive(false);
    }

    public void SetNotificationText(string text) {
        notificationText.text = text;
    }

    public void ShowHeadupNotification(string text, int seconds) {
        headupNotifictionText.text = text;
        headupNotifictionObject.SetActive(true);

        StartCoroutine(HideHeadupNotification(seconds));
    }

    public IEnumerator HideHeadupNotification(int seconds) {
        yield return new WaitForSeconds(seconds);
        headupNotifictionObject.SetActive(false);
    }

    public void DisableUI() {
        canvasObject.gameObject.SetActive(false);
        canvasActive = false;
    }

    public void EnableUI() {
        canvasObject.gameObject.SetActive(true);
        canvasActive = true;
    }
}
