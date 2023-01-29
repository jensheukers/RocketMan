using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

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
    private UnityEvent<GameObject> onPlayerKilledEvent; /** gets triggered when player dies*/
    private UnityEvent<GameObject> onEnemyKilledEvent; /** gets triggered when enemy dies*/

    private UnityEvent<int> onQuestCompleted; /**onQuestCompleted event, gets triggered when a quest of int id is completed*/

    [SerializeField]
    private Image fadeImage; /**Image reference that is used to be able to fade between scenes*/

    [SerializeField]
    private Canvas timescoreCanvas;

    [SerializeField]
    private TMP_Text timeScoreText; /** Text reference to highscore text object*/

    private Transform pickupSpawnerTransform; /** We hold a spawning position for the SpawnPickup(RM_PickupSO data) method, we can set this in SetPickupSpawnPosition(Vector3 position) */

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

        timescoreCanvas.gameObject.SetActive(false);
    }


    private void LateUpdate() {
        if (timescoreCanvas.isActiveAndEnabled) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                timescoreCanvas.gameObject.SetActive(false);
            }
        }

        //Check if mission is done if so return to main menu
        if (currentMission != null) {
            if (currentMission.IsLoaded() && currentMission.IsDone()) {
                Destroy(this.GetComponent<RM_Mission>());
                RM_Mission m = ChangeMission(mainMenu);

                //Set highscore
                timeScoreText.text = Time.timeSinceLevelLoad.ToString();
                timescoreCanvas.gameObject.SetActive(true);

            }
            else {
                if (currentMission.MissionData().GetCurrentQuest()) {
                    if (currentMission.MissionData().GetCurrentQuest().IsCompleted()) {
                        onQuestCompleted.Invoke(currentMission.MissionData().GetCurrentQuestID());
                    }
                }
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
            Destroy(this.GetComponent<RM_Mission>());

            currentMission = null;
        }

        currentMission = gameObject.AddComponent<RM_Mission>();

        onPlayerKilledEvent = new UnityEvent<GameObject>();
        onEnemyKilledEvent = new UnityEvent<GameObject>();

        onQuestCompleted = new UnityEvent<int>();

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
     * @brief Stop the current mission
     * */
    public static void StopCurrentMission() {
        if (GetCurrentMission()) {
            GetCurrentMission().StopMission();
        }
    }

    /*
     * @brief Triggers onPlayerKilledEvent
     */
    public static void OnPlayerKilled(GameObject player) {
        if (!_instance) return;
        _instance.onPlayerKilledEvent.Invoke(player);
    }


    /*
     * @brief Triggers onEnemyKilledEvent
     */
    public static void OnEnemyKilled(GameObject enemy) {
        if (!_instance) return;
        _instance.onEnemyKilledEvent.Invoke(enemy);
    }


    /**
     * @brief Sets player healthcomponent health to zero
     * @param GameObject
     */
    public static void KillPlayer() {
        if (!_instance) return;

        GameObject player = GameObject.FindGameObjectWithTag("RM_Player");

        if (!player) return;

        RM_HealthComponent healthComponent = player.GetComponent<RM_HealthComponent>();
        if (healthComponent) healthComponent.Damage(healthComponent.GetMaxHealth());
        
    }

    /*
     * @brief Adds to  onPlayerKilledEvent
     * @param UnityAction
     */
    public static void AddOnPlayerKilled(UnityAction<GameObject> action) {
        if (!_instance) return;
        _instance.onPlayerKilledEvent.AddListener(action);
    }


    /*
     * @brief Adds to onEnemyKilledEvent
     * @param UnityAction
     */
    public static void AddOnEnemyKilled(UnityAction<GameObject> action) {
        if (!_instance) return;
        _instance.onEnemyKilledEvent.AddListener(action);
    }

    /**
    * @brief Adds event to onquestcompleted
    * @param int questId
    * @param UnityAction function, param will be the quest id
    **/
    public static void AddOnQuestCompleted(int questId, UnityAction<int> action) {
        if (!_instance) return;
        _instance.onQuestCompleted.AddListener(action);
    }

    public static void SetQuestTaskFlag(int questId, int taskId, string flagName, bool value) {
        if (!_instance) return;
        if (_instance.currentMission) {
            if (_instance.currentMission.MissionData().GetQuest(questId)) {
                _instance.currentMission.MissionData().GetQuest(questId).GetTask(taskId).SetFlag(flagName, value);
            }
            else {
                Debug.LogWarning("AddOnQuestCompleted: Quest not found");
            }
        }
        else {
            Debug.LogWarning("AddOnQuestCompleted: no currentMission present");
        }
    }

    /*
     * Retrieves the current mission
     * @return RM_Mission
     */
    public static RM_Mission GetCurrentMission() {
        if (!_instance) return null;
        return _instance.currentMission;
    }

    /**
     * @brief returns if gamestate instance exists
     */
    public static bool InstanceExists() {
        return _instance;
    }
    /**
     * Quits the application
     */ 
    public static void QuitApplication() {
        Application.Quit();
    }

    public static void ReturnToMainMenu() {
        if (_instance.mainMenu) _instance.ChangeMission(_instance.mainMenu);
    }

    /*
     * @brief Plays a cutscene
     */
    public static void PlayCutscene(RM_Cutscene cutscene) {

        GameObject player = GameObject.FindGameObjectWithTag("RM_Player");

        if (player) {
            player.GetComponent<RM_UIManager>().DisableUI(); //Disable UI
            player.GetComponent<RM_CharacterController>().enabled = false;
            player.GetComponent<RM_WeaponManager>().enabled = false;

            cutscene.AddOnCutsceneStop(() => {
                player.GetComponent<RM_UIManager>().EnableUI(); //Re Enable UI
                player.GetComponent<RM_CharacterController>().enabled = true;
                player.GetComponent<RM_WeaponManager>().enabled = true;
            });
        }

        cutscene.Play();
    }

    /**
    * Fades to screen to black
    */
    private IEnumerator FadeScreenInternal(Color from, Color to, float fadeDuration) {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration) {
            elapsedTime += Time.deltaTime;
            _instance.fadeImage.color = Color.Lerp(from, to, elapsedTime / fadeDuration);
            yield return null;
        }
    }

    public static void FadeScreen(Color from, Color to, float fadeDuration) {
        if (!_instance) return;
        _instance.StartCoroutine(_instance.FadeScreenInternal(from, to, fadeDuration)); 
    }

    /*
     * @brief Method to set _instance.pickupSpawnerTransform position, this position is used for spawning pickups through gamestate, because our events can only handle 1 parameter
     * @param Vector3 position
     */
    public static void SetPickupSpawnerTransform(Transform t) {
        _instance.pickupSpawnerTransform = t;
    }

    /*
     * @brief Spawns pickup using RM_PickupSO data, spawns it at the last set _instance.pickupSpawnerTransform, make sure to call SetPickupSpawnerTransform() first
     * @param RM_PickupSO data
     */
    public static void SpawnPickup(RM_PickupSO data) {
        if (!_instance.pickupSpawnerTransform) return;
        GameObject g = new GameObject();
        g.transform.position = _instance.pickupSpawnerTransform.position;

        BoxCollider bc = g.AddComponent<BoxCollider>();
        bc.isTrigger = true;

        RM_Pickup p = g.AddComponent<RM_Pickup>();
        p.SetPickupScriptableObject(data);
    }
}
