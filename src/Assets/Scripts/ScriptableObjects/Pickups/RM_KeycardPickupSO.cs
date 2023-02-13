using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Keycard pickup scriptable object
/// </summary>
[CreateAssetMenu(fileName = "New Keycard Pickup", menuName = "Pickups/KeycardPickup")]
public class RM_KeycardPickupSO : RM_PickupSO {
    public override void OnPickup(Collider collider) {
       RM_Inventory inventory = collider.GetComponent<RM_Inventory>();
       if (inventory) {
            inventory.AddItem(this.GetType().Name);
       }

       base.OnPickup(collider);
    }
}
