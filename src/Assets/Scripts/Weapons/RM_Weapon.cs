using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RM_Weapon : MonoBehaviour {
    [SerializeField]
    private Transform RightHandPivot; /** The pivot where the right hand should attach to*/

    [SerializeField]
    private Transform LeftHandPivot; /** The pivot where the left hand should attach to*/

    [SerializeField]
    private GameObject projectilePrefab; /** The prefab of the projectile **/

    [SerializeField]
    protected Transform barrelEnd; /** The barrelEnd transform, from here the projectile gets instantiated*/

    [SerializeField]
    private int ammo; /**Amount of ammo*/

    [SerializeField]
    private float shootForce = 100f; /** Force amount for shot*/

    protected bool canShoot; /** if true, a shot can be instantiated if false not*/

    [SerializeField]
    private float timeBetweenShots = 1; /** The time between shots*/

    private void Start() {
        canShoot = true;
    }

    /**
     * @brief Instantiates a projectile and fires it in the barrelend direction
     * */
    public void Shoot() {
        if (ammo <= 0 || !canShoot) return;
        ammo--;

        GameObject projectile = Instantiate(projectilePrefab, barrelEnd.position, barrelEnd.rotation);
        projectile.GetComponent<Rigidbody>().velocity = barrelEnd.up * (shootForce);

        canShoot = false;

        StartCoroutine(ResetShootTimer());
    }

    /**
     * Resets the shoot timer, then sets canshoot to true
     */
    private IEnumerator ResetShootTimer() {
        yield return new WaitForSeconds(timeBetweenShots);

        canShoot = true;
    }

    /**
     * Adds ammo to the weapon
     * @param int
     */
    public void AddAmmo(int amount) {
        ammo += amount;
    }

    /**
     * gets the weapon ammo amount
     * @return int
     */
    public int GetAmmo() {
        return ammo;
    }

    public Transform GetRightHandPivot() {
        return RightHandPivot;
    }

    public Transform GetLeftHandPivot() {
        return LeftHandPivot;
    }


    //Editor methods
    private void OnDrawGizmos() {
        Debug.DrawLine(barrelEnd.position, barrelEnd.position + (barrelEnd.up * 5), Color.green);
    }
}
