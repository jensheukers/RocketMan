using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moves a object to desired offset position upon quest completion
/// </summary>
public class RM_QuestSetFlagAction : MonoBehaviour {
    [SerializeField]
    private int questId;

    [SerializeField]
    private int taskId;

    [SerializeField]
    private string flagName;

    [SerializeField]
    private bool value;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "RM_Player") {
            RM_GameState.SetQuestTaskFlag(questId, taskId, flagName, value);
            Destroy(gameObject);
        }
    }
}
