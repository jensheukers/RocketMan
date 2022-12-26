using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    public void OnNext() {
        List<RM_MissionSO> list = RM_GameState.GetMissions();

        if (currentMissionIndex + 1 != list.Count) {
            currentMissionIndex++;
            OnChangeMission(list, currentMissionIndex);
        }
    }

    public void OnPrevious() {
        List<RM_MissionSO> list = RM_GameState.GetMissions();

        if (currentMissionIndex - 1 > -1) {
            currentMissionIndex--;
            OnChangeMission(list, currentMissionIndex);
        }
    }

    public void OnChangeMission(List<RM_MissionSO> list, int index) {
        RM_MissionSO missionData = list[index];
        missionText.text = missionData.missionName;

        //TO:DO implement imgae
    }
}
