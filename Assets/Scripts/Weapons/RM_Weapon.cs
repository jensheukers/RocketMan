using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RM_Weapon : MonoBehaviour {
    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField]
    protected Transform barrelEnd;

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

        GameObject projectile = Instantiate(projectilePrefab, barrelEnd.position, barrelEnd.rotation);
        projectile.GetComponent<Rigidbody>().velocity = barrelEnd.up * (shootForce);
    }

    public void AddAmmo(int amount) {
        ammo += amount;
    }

    private void OnDrawGizmos() {
        Debug.DrawLine(barrelEnd.position, barrelEnd.position + (barrelEnd.up * 5), Color.green);
    }
}
