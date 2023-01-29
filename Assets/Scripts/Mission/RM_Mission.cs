using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// RM_Mission acts as a sort of wrapper for the RM_MissionSO object. handles communication between gamestate and the current mission data
/// </summary>
public class RM_Mission : MonoBehaviour {
    private RM_MissionSO data; /** MissionDataSO Reference */

    private AsyncOperation operation; /** The coroutine operation*/

    /**
     * @brief  Loads and start the mission, should be called first before any other runtime action is performed on the mission
     */
    public IEnumerator LoadAndStartMission(RM_MissionSO data) {
        this.data = data;

        operation = SceneManager.LoadSceneAsync(data.missionSceneName);

        while (!IsLoaded()) {
            yield return 0;
        }

        data.OnStart();
    }

    /**
     * @brief Calls the OnStop function on mission data ScriptableObject 
     */
    public void StopMission() { data.OnStop(); }

    /**
     * @brief returns data.IsDone
     * @return bool
     */
    public bool IsDone() { return data.IsDone(); }

    /**
     * @brief Calls the OnUpdate function on mission data ScriptableObject 
     */
    public void Update() { data.OnUpdate(); }

    /**
     * Returns the mission data
     */
    public RM_MissionSO MissionData() {
        return data;
    }

    /*
     * Returns true if async loading operation is done
     */
    public bool IsLoaded() {
        return operation.isDone;
    }
}
