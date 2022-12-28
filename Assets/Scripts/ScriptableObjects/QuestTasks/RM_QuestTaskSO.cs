using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Task", menuName = "Missions/Quest/QuestTask")]
public class RM_QuestTaskSO : ScriptableObject {
    [SerializeField]
    public string taskName;

    [SerializeField]
    public string taskDescription;

    private bool completed;

    public virtual void OnTaskStart() {
        completed = false;
    }

    public virtual void OnTaskUpdate() { }

    protected virtual void OnTaskComplete() {
        completed = true;
    }

    public virtual void OnEnemyKilled(GameObject enemy) { }
    public virtual void OnPlayerKilled(GameObject enemy) { }

    public bool IsCompleted() {
        return completed;
    }
}