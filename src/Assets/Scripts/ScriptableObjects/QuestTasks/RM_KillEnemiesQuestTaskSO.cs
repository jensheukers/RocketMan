using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Task(RM_KillEnemiesQuestTask)", menuName = "Missions/Quest/QuestTaskKillEnemies")]
public class RM_KillEnemiesQuestTaskSO : RM_QuestTaskSO {
    [SerializeField]
    private float enemiesToKill = 5;

    private float enemiesKilled;

    public override void OnTaskStart() {
        base.OnTaskStart();
        enemiesKilled = 0;
    }

    protected override void OnTaskComplete() {
        base.OnTaskComplete();
    }

    public override void OnEnemyKilled(GameObject enemy) {
        if (enemiesKilled + 1 == enemiesToKill) OnTaskComplete();
        else enemiesKilled++;
    }
}

