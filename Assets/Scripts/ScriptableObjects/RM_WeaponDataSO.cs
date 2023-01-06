using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// RM_MissionSO is a ScriptableObject containing weapon data
/// </summary>
[CreateAssetMenu(fileName = "New WeaponData", menuName = "Weapon/WeaponData")]
public class RM_WeaponDataSO : ScriptableObject {
    public string weaponName; /** The name of the weapon*/
    public GameObject weaponPrefab; /** reference to the weapon prefab*/
}
