using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RM_QuestTaskSO_QuestFlag {
    public string flagName;
    public bool flagValue;
}

[CreateAssetMenu(fileName = "New Task", menuName = "Missions/Quest/QuestTask")]
public class RM_QuestTaskSO : ScriptableObject {
    [SerializeField]
    public string taskName;

    [SerializeField]
    public string taskDescription;

    [SerializeField]
    private List<RM_QuestTaskSO_QuestFlag> flagList;

    private bool completed;

    public virtual void OnTaskStart() {
        completed = false;

        //Set all flag states to false
        for (int i = 0; i < flagList.Count; i++) {
            flagList[i].flagValue = false;
        }

        Debug.Log("Quest Task: " + taskName + " Started!");
    }

    public virtual void OnTaskUpdate() {
        if (completed) return;

        //RM_QuestFlag_CompleteTask is the default flag name for completing quest task
        //Should be refactored
        for (int i = 0; i < flagList.Count; i++) {
            if (flagList[i].flagName == "RM_QuestFlag_CompleteTask") {
                if (flagList[i].flagValue == true) OnTaskComplete();
            }
        }
    }

    protected virtual void OnTaskComplete() {
        completed = true;
        Debug.Log("Quest Task: " + taskName + " Completed!");
    }

    public virtual void OnEnemyKilled(GameObject enemy) { }
    public virtual void OnPlayerKilled(GameObject enemy) { }

    public bool IsCompleted() {
        return completed;
    }

    public void SetFlag(string flagName, bool value) {
        for (int i = 0; i < flagList.Count; i++) {
            if (flagList[i].flagName == flagName) flagList[i].flagValue = value;
        }
    }
}