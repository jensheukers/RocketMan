using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RM_WeaponManager : MonoBehaviour {
    [SerializeField]
    private Transform weaponPivot;

    [SerializeField]
    private RM_WeaponDataSO currentWeaponData;

    private RM_Weapon currentWeapon;

    private void Start() {
        SetCurrentWeapon(currentWeaponData);
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) ShootCurrentWeapon();
    }

    public RM_WeaponDataSO GetCurrentWeaponData() {
        return currentWeaponData;
    }

    public RM_Weapon GetCurrentWeapon() {
        return currentWeapon;
    }

    public void SetCurrentWeapon(RM_WeaponDataSO weaponData) {
        if (currentWeapon) Destroy(currentWeapon);
        GameObject _spawned = Instantiate(weaponData.weaponPrefab, weaponPivot.position, weaponPivot.rotation, weaponPivot);
        currentWeapon = _spawned.GetComponent<RM_Weapon>();
    }

    public void ShootCurrentWeapon() {
        if (currentWeapon) currentWeapon.Shoot();
    }
}
