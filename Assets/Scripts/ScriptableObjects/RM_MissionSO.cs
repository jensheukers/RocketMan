using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// RM_MissionSO is a ScriptableObject containing mission data
/// </summary>
[CreateAssetMenu(fileName = "New Mission", menuName = "Missions/Mission")]
public class RM_MissionSO : ScriptableObject {
    public string missionName = "Unnamed Mission"; //* Mission name definition */
    public string missionSceneName = "SampleScene"; /* Mission scene name reference */

    //Trigger keys
    public string endMissionTriggerKey = "RM_Trigger_Mission_End"; /* The key of the trigger that marks the end of the mission */
    public bool missionHasEnd = true;
    public bool unlockCursorInStart = true;

    private bool done; /* Gets set to true in OnStart, and set to false in OnStop */

    //Questing setup
    [SerializeField]
    private List<RM_QuestSO> quests;
    private RM_QuestSO currentQuest;
    private int currentQuestIndex;

    /*
     * @brief Gets called by RM_Mission class when mission data is to be started.
     */
    public virtual void OnStart() {
        currentQuest = null;
        done = false;

        if (unlockCursorInStart) Cursor.lockState = CursorLockMode.None;

        //Default Level setup
        RM_Trigger endMissionTrigger = FindTriggerByName(endMissionTriggerKey);

        if (!endMissionTrigger && missionHasEnd) {
            Debug.LogWarning("End of mission has not been set up, please make sure to create a trigger and set the triggerkey to " + endMissionTriggerKey);
        }
        else if (endMissionTrigger) {
            endMissionTrigger.onTriggerEnterEvent.AddListener((Collider other) => {
                if (other.tag == "RM_Player") {
                    OnStop();
                }
            });
        }

        //Start quest
        if (quests.Count > 0) {
            currentQuestIndex = 0;
            StartQuest(currentQuestIndex);
        }

        RM_GameState.AddOnEnemyKilled((GameObject enemy) => {
            if (currentQuest) currentQuest.OnEnemyKilled(enemy);
        });

        RM_GameState.AddOnPlayerKilled((GameObject player) => {
            if (currentQuest) currentQuest.OnPlayerKilled(player);
        });
    }

    /**
    * @brief Gets called by default endMissionTrigger
    */
    public virtual void OnStop() {
        done = true;
    }

    /**
     * @brief Gets called every frame by RM_Mission
     */
    public virtual void OnUpdate() {
        if (currentQuest) {
            if (currentQuest.IsCompleted()) {
                //Start next quest
                if (currentQuestIndex + 1 < quests.Count) {
                    currentQuestIndex++;
                    StartQuest(currentQuestIndex);
                }
            }
            else {
                currentQuest.OnQuestUpdate();
            }
        }
    }

    /**
     * @brief Returns true if mission is done, else returns false
     * @return bool
    */
    public bool IsDone() {
        return done;
    }
    public RM_QuestSO GetCurrentQuest() {
        return currentQuest;
    }

    public RM_QuestSO GetQuest(int id) {
        return quests[id]; 
    }

    /**
     * @brief Finds a trigger by keyname
     * @return RM_Trigger
     */
    protected RM_Trigger FindTriggerByName(string triggerKey) {
        GameObject[] list = GameObject.FindGameObjectsWithTag("RM_Trigger");

        foreach (GameObject go in list) {
            if (go.GetComponent<RM_Trigger>() && go.GetComponent<RM_Trigger>().GetTriggerKey() == triggerKey) {
                return go.GetComponent<RM_Trigger>();
            } 
        }

        return null;
    }

    /**
     * @brief Starts a quest
     * @param int id
     */
    private void StartQuest(int id) {
        currentQuest = quests[id];
        currentQuest.OnStartQuest();
    }
}