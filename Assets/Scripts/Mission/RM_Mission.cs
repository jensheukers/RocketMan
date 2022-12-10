using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// RM_Mission holds all variables of the current mission, and also handles multiple events.
/// </summary>
public class RM_Mission : MonoBehaviour {
    private RM_MissionSO data; /** MissionDataSO Reference */

    public IEnumerator LoadAndStartMission(RM_MissionSO data) {
        this.data = data;

        AsyncOperation async = SceneManager.LoadSceneAsync(data.missionSceneName);

        while (!async.isDone) {
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
}
