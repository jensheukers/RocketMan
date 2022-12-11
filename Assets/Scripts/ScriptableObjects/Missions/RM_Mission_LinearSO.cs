using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// RM_MissionSO is a derrived ScriptableObject containing mission data
/// Linear missions are missions where the player has to get from point a to point b and passes through checkpoints.
/// </summary>
[CreateAssetMenu(fileName = "New Mission", menuName = "Missions/Mission_Linear")]
public class RM_Mission_LinearSO : RM_MissionSO {
    public List<string> checkPointTriggerKeys; /** The trigger keys of checkpoint triggers*/

    public GameObject playerPrefab; /** The prefab of the player to initiate*/
}
