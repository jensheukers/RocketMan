using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Quest Scriptable Object
[CreateAssetMenu(fileName = "New Quest", menuName = "Missions/Quest/Quest")]
public class RM_QuestSO : ScriptableObject {
    private RM_QuestTaskSO currentTask;
    private int currentTaskId;

    [SerializeField]
    private string questName;

    [SerializeField]
    private List<RM_QuestTaskSO> tasks;
    private bool completed;

    private UnityEvent onQuestCompleted;

    public void OnStartQuest() {
        completed = false;
        if (tasks.Count > 0) {
            SetTask(0);
        }
    }

    public void OnQuestUpdate() {
        if (currentTask) {
            currentTask.OnTaskUpdate();

            if (currentTask.IsCompleted()) {
                if (currentTaskId + 1 >= tasks.Count) {
                    Debug.Log("test");
                    completed = true;
                    onQuestCompleted.Invoke();
                }
                else {
                    SetTask(currentTaskId + 1);
                }
            }
        }
    }

    public void OnEnemyKilled(GameObject enemy) {
        if (currentTask != null) currentTask.OnEnemyKilled(enemy);
    }

    public void OnPlayerKilled(GameObject player) {
        if (currentTask != null) currentTask.OnPlayerKilled(player);
    }

    public void AddOnQuestCompleted(UnityAction action) {
        onQuestCompleted.AddListener(action);
    }

    public bool IsCompleted() {
        return completed;
    }

    private void SetTask(int id) {
        currentTask = tasks[id];
        currentTaskId = id;

        currentTask.OnTaskStart();
    }

    public RM_QuestTaskSO GetTask(int id) {
        return tasks[id];
    }

    public RM_QuestTaskSO GetCurrentTask() {
        return currentTask;
    }

    public string GetQuestName() {
        return questName;
    }
}
