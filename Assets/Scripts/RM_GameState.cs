using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// RM_GameState is the main gamestate class for RocketMan, it must never be destroyed during the whole instance of the program.
/// 
/// It handles mission changing, and it responsible for talking to multiple systems.
/// </summary>
public class RM_GameState : MonoBehaviour {
    private static RM_GameState _instance;

    private RM_Mission currentMission; //* Reference to the current mission*/

    [SerializeField]
    private RM_MissionSO mainMenu; //* Reference to the main menu RM_MissionSO */

    [SerializeField]
    private List<RM_MissionSO> missions; //* List of missions. */

    //Notify events (Which objects can cling on to)
    [SerializeField]
    private UnityAction<GameObject> onPlayerKilledEvent; /** gets triggered when player dies*/

    [SerializeField]
    private UnityAction<GameObject> onEnemyKilledEvent; /** gets triggered when player dies*/

    /*
    * @brief Start Method, we make sure that this object will never be destroyed while running the program and set variables.
    * @return void
    */
    void Awake() {
        if (_instance) {
            Destroy(this.gameObject);
            Debug.LogError("Only 1 instance of RM_GameState can be present");
            return;
        }

        _instance = this;

        DontDestroyOnLoad(this.gameObject);

        if (!mainMenu) Debug.LogError("RM_GameState: " + "main menu not set!");
        else ChangeMission(mainMenu);
    }


    private void Update() {

        //Check if mission is done if so return to main menu
        if (currentMission) {
            if (currentMission.IsDone()) {
                ChangeMission(mainMenu);
            }
        } 
    }

    /*
    * @brief Changes the mission and returns new RM_Mission instance.
    * @param RM_MissionSO mission data
    * @return RM_Mission
    */
    public RM_Mission ChangeMission(RM_MissionSO data) {
        if (currentMission) {
            currentMission.StopMission();
        }
        currentMission = gameObject.AddComponent<RM_Mission>();

        StartCoroutine(currentMission.LoadAndStartMission(data));
        return currentMission;
    }

    /*
    * @brief Tries to find missiondata that contains parameter value, if so returns new RM_Mission instance
    * @param string mission name
    * @return RM_Mission
    */
    public RM_Mission ChangeMission(string name) {
        foreach (RM_MissionSO data in missions) {
            if (data.name == name) return ChangeMission(data);
        }
        return null;
    }

    //Static functions
    public static void ChangeMissionStatic(RM_MissionSO data) {
        _instance.ChangeMission(data);
    }

    public static void ChangeMissionStatic(string name) {
        _instance.ChangeMission(name);
    }

    /*
     * @brief Returns the mission list
     * @return List<RM_MissionSO>
     */
    public static List<RM_MissionSO> GetMissions() {
        return _instance.missions;
    }

    /*
     * @brief Triggers onPlayerKilledEvent
     */
    public static void OnPlayerKilled(GameObject player) {
        if (!_instance) return;
        _instance.onPlayerKilledEvent(player);
    }


    /*
     * @brief Triggers onEnemyKilledEvent
     */
    public static void OnEnemyKilled(GameObject enemy) {
        if (!_instance) return;
        _instance.onEnemyKilledEvent(enemy);
    }


    /*
     * @brief Adds to  onPlayerKilledEvent
     * @param UnityAction
     */
    public static void AddOnPlayerKilled(UnityAction<GameObject> action) {
        if (!_instance) return;
        _instance.onPlayerKilledEvent += action;
    }


    /*
     * @brief Adds to onEnemyKilledEvent
     * @param UnityAction
     */
    public static void AddOnEnemyKilled(UnityAction<GameObject> action) {
        if (!_instance) return;
        _instance.onEnemyKilledEvent += action;
    }
}
