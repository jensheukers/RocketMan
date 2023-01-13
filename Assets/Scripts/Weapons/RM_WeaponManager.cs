using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RM_WeaponManager : MonoBehaviour {
    [SerializeField]
    private Transform weaponPivot;

    [SerializeField]
    private RM_WeaponDataSO currentWeaponData;

    [SerializeField]
    private Transform RightBoneIKConstraintTransform; /** Refernce to the right Two-IK Bone Constraint Target transform*/

    [SerializeField]
    private Transform LeftBoneIKConstraintTransform; /** Refernce to the left Two-IK Bone Constraint Target transform*/

    private RM_Weapon currentWeapon;

    private void Start() {
        currentWeapon = null;
        SetCurrentWeapon(currentWeaponData);
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0) && Time.timeScale != 0) ShootCurrentWeapon();

        if (currentWeapon) {
            RightBoneIKConstraintTransform.position = currentWeapon.GetRightHandPivot().position;
            RightBoneIKConstraintTransform.rotation = currentWeapon.GetRightHandPivot().rotation;

            LeftBoneIKConstraintTransform.position = currentWeapon.GetLeftHandPivot().position;
            LeftBoneIKConstraintTransform.rotation = currentWeapon.GetLeftHandPivot().rotation;
        }
    }

    public RM_WeaponDataSO GetCurrentWeaponData() {
        return currentWeaponData;
    }

    public RM_Weapon GetCurrentWeapon() {
        return currentWeapon;
    }

    public void SetCurrentWeapon(RM_WeaponDataSO weaponData) {
        if (!weaponData) return;
        if (currentWeapon) Destroy(currentWeapon);
        GameObject _spawned = Instantiate(weaponData.weaponPrefab, weaponPivot.position, weaponPivot.rotation, weaponPivot);
        currentWeapon = _spawned.GetComponent<RM_Weapon>();
    }

    public void ShootCurrentWeapon() {
        if (currentWeapon) currentWeapon.Shoot();
    }
}
