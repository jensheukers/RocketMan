using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RM_Turret : RM_Weapon {
    [SerializeField]
    private Transform mount;

    [SerializeField]
    private float mountRotateSpeed = 250f;

    [SerializeField]
    private Transform gun;


    [SerializeField]
    private float gunRotateSpeed = 250f;

    [SerializeField]
    private float minGunRotateAmount = -40f;

    [SerializeField]
    private float maxGunRotateAmount = 75f;

    [SerializeField]
    private Transform target;

    private bool canShoot;

    [SerializeField]
    private float timeBetweenShots;


    [SerializeField]
    private float aimDistance = 100f;


    private void Start() {
        canShoot = true;
    }

    private void Update() {
        if (target) {

            //First part - Rotates the base of the turret
            Quaternion lookRotation = Quaternion.LookRotation(target.position - mount.position, Vector3.up);
            mount.rotation = Quaternion.RotateTowards(mount.rotation, lookRotation, Time.deltaTime * mountRotateSpeed);
            mount.localEulerAngles = new Vector3(0, 0, mount.localEulerAngles.z);

            Quaternion gunlookRotation = Quaternion.LookRotation(target.position - mount.position, Vector3.up) * Quaternion.AngleAxis(-90, Vector3.right);
            gun.rotation = Quaternion.RotateTowards(gun.rotation, gunlookRotation, Time.deltaTime * gunRotateSpeed);

            gun.localEulerAngles = new Vector3(gun.localEulerAngles.x, 0, 0);

            RaycastHit hit;
            if (Physics.Raycast(barrelEnd.position, barrelEnd.up, out hit, aimDistance)) {
                if (hit.transform.root == target && canShoot) {
                    Shoot();
                    canShoot = false;

                    StartCoroutine(ResetShootTimer());
                }
            }
           
        }
    }
    private IEnumerator ResetShootTimer() {
        yield return new WaitForSeconds(timeBetweenShots);

        canShoot = true;
    }

    public void SetTarget(Transform target) {
        this.target = target;
    }
}
