using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sets a questflag
/// </summary>
public class RM_QuestSetFlagAction : MonoBehaviour {
    [SerializeField]
    private int questId; /***The id of the quest*/

    [SerializeField]
    private int taskId; /**The id of the quest task*/

    [SerializeField]
    private string flagName; /**Name of the quest flag*/

    [SerializeField]
    private bool value; /** Value to set*/

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "RM_Player") {
            RM_GameState.SetQuestTaskFlag(questId, taskId, flagName, value);
            Destroy(gameObject);
        }
    }
}
