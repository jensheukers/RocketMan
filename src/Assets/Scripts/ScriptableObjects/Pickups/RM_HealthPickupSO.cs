using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Health pickup scriptable object
/// </summary>
[CreateAssetMenu(fileName = "New Health Pickup", menuName = "Pickups/HealthPickup")]
public class RM_HealthPickupSO : RM_PickupSO {
    [SerializeField]
    private int healingAmount;

    public override void OnPickup(Collider collider) {
        RM_HealthComponent hc = collider.GetComponent<RM_HealthComponent>();

        if (hc) hc.Heal(healingAmount);

        base.OnPickup(collider);
    }
}
