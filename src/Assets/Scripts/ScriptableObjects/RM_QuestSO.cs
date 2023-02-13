using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Quest scriptable object
/// </summary>
[CreateAssetMenu(fileName = "New Quest", menuName = "Missions/Quest/Quest")]
public class RM_QuestSO : ScriptableObject {
    private RM_QuestTaskSO currentTask; /** The current task*/
    private int currentTaskId; /** The current task id*/

    [SerializeField]
    private string questName; /** The quest name*/

    [SerializeField]
    private List<RM_QuestTaskSO> tasks; /** The tasks of the quest*/
    private bool completed; /** Gets set to true upon completion*/

    private void OnEnable() {
        completed = false;
        currentTaskId = 0;
    }

    /**
     * @brief Starts the quest
     */
    public void OnStartQuest() {
        if (tasks.Count > 0) {
            SetTask(0);
        }
    }

    /**
     * @brief Updates the quest
     */
    public void OnQuestUpdate() {
        if (currentTask) {
            currentTask.OnTaskUpdate();

            if (currentTask.IsCompleted()) {
                if (currentTaskId + 1 >= tasks.Count) {
                    completed = true;
                }
                else {
                    SetTask(currentTaskId + 1);
                }
            }
        }
    }

    /**
     * @brief Gets called when a enemy is killed
     * @param GameObject player
     */
    public void OnEnemyKilled(GameObject enemy) {
        if (currentTask != null) currentTask.OnEnemyKilled(enemy);
    }

    /**
    * @brief Gets called when the player is killed
    * @param GameObject player
    */
    public void OnPlayerKilled(GameObject player) {
        if (currentTask != null) currentTask.OnPlayerKilled(player);
    }

    /**
     * @brief Returns true if quest is completed
     */
    public bool IsCompleted() {
        return completed;
    }

    /**
     * @brief Sets the current task
     */
    private void SetTask(int id) {
        currentTask = tasks[id];
        currentTaskId = id;

        currentTask.OnTaskStart();
    }

    /**
     * @brief Gets task from list
     */
    public RM_QuestTaskSO GetTask(int id) {
        return tasks[id];
    }

    /**
     * @brief Gets the current task
     */
    public RM_QuestTaskSO GetCurrentTask() {
        return currentTask;
    }

    /**
     * @brief Gets the quest name
     */ 
    public string GetQuestName() {
        return questName;
    }
}
