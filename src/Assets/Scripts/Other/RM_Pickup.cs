using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pickups are triggers that display a RM_PickupSO
/// </summary>
public class RM_Pickup : RM_Trigger {
    [SerializeField]
    private RM_PickupSO pickupScriptableObject; /*** The ScriptableObject reference*/

    [SerializeField]
    private float rotateSpeed = 100f; /** The spsed to Rotate*/

    protected override void Start() {
        base.Start();
        onTriggerEnterEvent.AddListener(OnPickup);

        //Spawn pickup prefab 
        SpawnPickupSOPrefab();
    }

    protected virtual void Update() {
        transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0));
    }

    /**
     * @brief Spawns the pickup prefab
     */
    protected virtual void SpawnPickupSOPrefab() {
        if (!pickupScriptableObject) return;
        Instantiate(pickupScriptableObject.GetPrefab(), transform);
    }

    protected virtual void OnPickup(Collider collider) {
        //Log to headup that we picked up item
        RM_UIManager um = collider.GetComponent<RM_UIManager>();
        if (um) um.ShowHeadupNotification("Picked up: " + pickupScriptableObject.GetPickupName(), 2); //Kinda hard coded the time but its fine :)

        Destroy(this.gameObject);
        pickupScriptableObject.OnPickup(collider);
    }

    /**
     * @brief Sets the pickup scriptableobject reference
     * @param RM_PickupSO ScriptableObject
     */
    public void SetPickupScriptableObject(RM_PickupSO data) {
        pickupScriptableObject = data;
        SpawnPickupSOPrefab();
    }
}
