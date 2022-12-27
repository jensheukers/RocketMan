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

    private string currentCheckpointTriggerKey;

    public override void OnStart() {
        base.OnStart();

        //Set up gamestate events
        RM_GameState.AddOnPlayerKilled((GameObject player) => {
            RM_Trigger curCheckpoint = FindTriggerByName(currentCheckpointTriggerKey);
            player.transform.position = curCheckpoint.transform.position;
        });

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

        currentCheckpointTriggerKey = checkPointTriggerKeys[0];
    }

    private void OnCheckPointReached(string triggerKey) {
        currentCheckpointTriggerKey = triggerKey;
    }
}
