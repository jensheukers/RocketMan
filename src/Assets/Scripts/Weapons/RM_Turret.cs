using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RM_Turret : RM_Weapon {
    [SerializeField]
    private Transform mount; /** The mount of the turret, rotates around the z axis*/

    [SerializeField]
    private float mountRotateSpeed = 250f; /** The speed at wich the mount rotates*/

    [SerializeField]
    private Transform gun; /** The gun transform , rotates around the x axis*/

    [SerializeField]
    private float gunRotateSpeed = 250f; /** The rotate speed of the gun*/

    [SerializeField]
    private Transform target; /** The target transform*/

    [SerializeField]
    private float aimDistance = 100f; /** The maximum distance to shoot*/

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
                if (hit.transform.root == target) {
                    Shoot();
                }
            }
           
        }
    }

    /**
     * @brief Sets the target transform
     * @param Transform
     */
    public void SetTarget(Transform target) {
        this.target = target;
    }
}
