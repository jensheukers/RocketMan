using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moves a object to desired offset position upon quest completion
/// </summary>
public class RM_QuestCompleteAction_MoveObject : MonoBehaviour {
    [SerializeField]
    private Vector3 desiredOffset = Vector3.up; /**The desired offset to add to position upon quest completion*/

    [SerializeField]
    private int questId; /** The id of the quest*/

    private void Start() {
        if (RM_GameState.InstanceExists()) {
            RM_GameState.AddOnQuestCompleted(questId, () => {
                transform.position = transform.position + desiredOffset;
            });
        }
        else {
            Debug.LogWarning("RM_QuestCompleteAction: Gamestate does not exist");
        }
    }
}
