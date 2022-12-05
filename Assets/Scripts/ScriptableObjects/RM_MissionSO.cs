using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// RM_MissionSO is a ScriptableObject containing mission data
/// </summary>
[CreateAssetMenu(fileName = "New Mission", menuName = "Missions/Mission")]
public class RM_MissionSO : ScriptableObject {
    public string missionName = "Unnamed Mission"; //* Mission name definition */
    public string missionSceneName = "SampleScene"; /* Mission scene name reference */

    public virtual void OnStart() { }
    public virtual void OnStop() { }

    public virtual void OnUpdate() { }
}
