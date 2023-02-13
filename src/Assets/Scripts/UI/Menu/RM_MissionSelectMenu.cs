using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Handles the mission select menu in the main menu
/// </summary>
public class RM_MissionSelectMenu : MonoBehaviour {
    private int currentMissionIndex;

    [SerializeField]
    private Image missionImage;

    [SerializeField]
    private TMP_Text missionText;

    private void Start() {
        currentMissionIndex = -1;

        OnNext();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            transform.gameObject.SetActive(false);
        }
    }

    /*
     * @brief Selects the next mission out of the list
     */
    public void OnNext() {
        List<RM_MissionSO> list = RM_GameState.GetMissions();

        if (currentMissionIndex + 1 != list.Count) {
            currentMissionIndex++;
            OnChangeMission(list, currentMissionIndex);
        }
    }

    /*
     * @brief Selects the previous mission out of the list
     */
    public void OnPrevious() {
        List<RM_MissionSO> list = RM_GameState.GetMissions();

        if (currentMissionIndex - 1 > -1) {
            currentMissionIndex--;
            OnChangeMission(list, currentMissionIndex);
        }
    }

    /*
     * @brief On Play button event
     */
    public void OnPlay() {
        List<RM_MissionSO> list = RM_GameState.GetMissions();

        RM_GameState.ChangeMissionStatic(list[currentMissionIndex]);
    }

    /*
     * @brief On Change Mission event
     */
    public void OnChangeMission(List<RM_MissionSO> list, int index) {
        missionText.text = list[index].missionName;

        missionImage.sprite = Resources.Load<Sprite>(list[index].imagePath);
    }
}
