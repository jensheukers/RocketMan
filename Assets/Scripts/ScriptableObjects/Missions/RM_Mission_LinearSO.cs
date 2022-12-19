using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// RM_MissionSO is a derrived ScriptableObject containing mission data
/// Linear missions are missions where the player has to get from point a to point b and passes through checkpoints.
/// </summary>
[CreateAssetMenu(fileName = "New Mission", menuName = "Missions/Mission_Linear")]
public class RM_Mission_LinearSO : RM_MissionSO {
    [SerializeField]
    private List<string> checkPointTriggerKeys; /** The trigger keys of checkpoint triggers*/

    private GameObject playerPrefab; /** The prefab of the player to initiate*/

    public override void OnStart() {
        base.OnStart();

        //Set up triggers
        for (int i = 0; i < checkPointTriggerKeys.Count; i++) {
            RM_Trigger trigger = FindTriggerByName(checkPointTriggerKeys[i]);

            if (!trigger) continue;

            trigger.onTriggerEnterEvent.AddListener((Collider other) => {
                if (other.tag == "RM_Player") {
                    Debug.Log("Checkpoint Reached");
                }
            });
        }
    }
}
