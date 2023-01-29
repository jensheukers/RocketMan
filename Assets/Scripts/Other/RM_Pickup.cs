using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RM_Pickup : RM_Trigger {
    [SerializeField]
    private RM_PickupSO pickupScriptableObject;

    [SerializeField]
    private float rotateSpeed = 100f;

    protected override void Start() {
        base.Start();
        onTriggerEnterEvent.AddListener(OnPickup);

        //Spawn pickup prefab 
        SpawnPickupSOPrefab();
    }

    protected virtual void Update() {
        transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0));
    }

    protected virtual void SpawnPickupSOPrefab() {
        if (!pickupScriptableObject) return;
        Instantiate(pickupScriptableObject.GetPrefab(), transform);
    }

    protected virtual void OnPickup(Collider collider) {
        Destroy(this.gameObject);
        pickupScriptableObject.OnPickup(collider);
    }

    public void SetPickupScriptableObject(RM_PickupSO data) {
        pickupScriptableObject = data;
        SpawnPickupSOPrefab();
    }
}
