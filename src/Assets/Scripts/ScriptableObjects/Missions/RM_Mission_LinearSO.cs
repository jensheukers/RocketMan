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

    private string currentCheckpointTriggerKey; /** The current checkpoint trigger key*/

    public override void OnStart() {
        base.OnStart();

       
        //Set up gamestate events
        RM_GameState.AddOnPlayerKilled((GameObject player) => {

            //Fade screens
            RM_GameState.FadeScreen(new Color(0,0,0,0), new Color(0,0,0,1), 0.5f);
            RM_GameState.FadeScreen(new Color(0,0,0,1), new Color(0,0,0,0), 5f);


            RM_TextSequence ts = player.GetComponent<RM_TextSequence>();
            ts.Play();

            RM_Trigger curCheckpoint = FindTriggerByName(currentCheckpointTriggerKey);
            player.transform.position = curCheckpoint.transform.position;

            //Reset health
            RM_HealthComponent hc = player.GetComponent<RM_HealthComponent>();
            hc.Heal(hc.GetMaxHealth());
        });

        //Set up triggers
        for (int i = 0; i < checkPointTriggerKeys.Count; i++) {
            RM_Trigger trigger = FindTriggerByName(checkPointTriggerKeys[i]);

            if (!trigger) continue;

            trigger.onTriggerEnterEvent.AddListener((Collider other) => {
                if (other.tag == "RM_Player") {
                    Debug.Log("Checkpoint Reached!");
                    OnCheckPointReached(trigger.GetTriggerKey());
                }
            });
        }

        //Set checkpoint
        currentCheckpointTriggerKey = checkPointTriggerKeys[0];
    }

    /*
     * @brief Gets called when checkpoint is reached 
     */
    private void OnCheckPointReached(string triggerKey) {
        currentCheckpointTriggerKey = triggerKey;
    }
}
