using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pickup scriptable object
/// </summary>
[CreateAssetMenu(fileName = "New Pickup", menuName = "Pickups/Pickup")]
public class RM_PickupSO : ScriptableObject {
    [SerializeField]
    private GameObject prefab;
    
    public GameObject GetPrefab() {
        return prefab;
    }

    public virtual void OnPickup(Collider collider) {}
}
