using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RM_Weapon : MonoBehaviour {
    [SerializeField]
    private string weaponName = "Weapon";

    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField]
    private Transform barrelEnd;

    [SerializeField]
    private int ammo;

    [SerializeField]
    private float shootForce = 100f;

    public void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Shoot();
        }
    }

    public void Shoot() {
        if (ammo <= 0) return;

        GameObject projectile = Instantiate(projectilePrefab, barrelEnd.transform.position, barrelEnd.transform.rotation);
        projectile.GetComponent<Rigidbody>().velocity = barrelEnd.transform.up * (shootForce);
    }

    public void AddAmmo(int amount) {
        ammo += amount;
    }
}
