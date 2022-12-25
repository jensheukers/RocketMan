using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RM_Projectile : MonoBehaviour {
    [SerializeField]
    private GameObject hitParticlePrefab; /** The particle system prefab to spawn on hit*/

    private int damage; /**Amount of damage this projectile inflicts, should be set directly after instantiating, default damage is 100% */

    private void Awake() {
        damage = 100; // Set default damage.
    }

    public void OnCollisionEnter(Collision collision) {
        GameObject particleSystem = Instantiate(hitParticlePrefab, transform.position, transform.rotation);

        if (collision.gameObject.GetComponent<RM_HealthComponent>()) {
            collision.gameObject.GetComponent<RM_HealthComponent>().Damage(damage);
        }

        Destroy(this.gameObject);
    }

    /**
     * @brief Sets the damage of the projectile
     * @param int
     */
    public void SetDamage(int amount) {
        damage = amount;
    }
}
