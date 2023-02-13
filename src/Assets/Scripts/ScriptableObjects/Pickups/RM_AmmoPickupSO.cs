using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ammo pickup scriptable object
/// </summary>
[CreateAssetMenu(fileName = "New Ammo Pickup", menuName = "Pickups/AmmoPickup")]
public class RM_AmmoPickupSO : RM_PickupSO {
    [SerializeField]
    private int ammoAmount = 20;

    public override void OnPickup(Collider collider) {
        RM_WeaponManager wm = collider.GetComponent<RM_WeaponManager>();

        if (wm && wm.GetCurrentWeapon()) wm.GetCurrentWeapon().AddAmmo(ammoAmount);

        base.OnPickup(collider);
    }
}
