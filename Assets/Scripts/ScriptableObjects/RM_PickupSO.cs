using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pickup scriptable object
/// </summary>
[CreateAssetMenu(fileName = "New Pickup", menuName = "Pickups/Pickup")]
public class RM_PickupSO : ScriptableObject {
    [SerializeField]
    private string pickupName = "Pickup"; /** The name of the pickup*/

    [SerializeField]
    private GameObject prefab; /** The prefab of the pickup*/
    
    /**
     * @brief Returns the prefab
     * @return GameObject
     */
    public GameObject GetPrefab() {
        return prefab;
    }


    /**
     * @brief Gets called on pickup
     * @param Collider the collider object 
     */
    public virtual void OnPickup(Collider collider) {}

    /**
     * @brief Returns the name of the pickup
     * @return string
     */
    public string GetPickupName() {
        return pickupName;
    }
}
