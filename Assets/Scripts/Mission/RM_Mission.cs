using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// RM_Mission holds all variables of the current mission, and also handles multiple events.
/// </summary>
public class RM_Mission : MonoBehaviour {
    private RM_MissionSO data; /** MissionDataSO Reference */

    public void LoadMission(RM_MissionSO data) {
        this.data = data;

        SceneManager.LoadScene(data.missionSceneName);
    }

    public void StartMission() { data.OnStart(); }

    public void StopMission() { data.OnStop(); }

    public void Update() { data.OnUpdate(); }
}
