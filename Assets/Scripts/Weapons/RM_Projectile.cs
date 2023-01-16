using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RM_Projectile : MonoBehaviour {
    [SerializeField]
    private GameObject hitParticlePrefab; /** The particle system prefab to spawn on hit*/

    [Range(0f, 100f)]
    public int damage = 100; /** The amount of damage to inflict on a RM_HealthComponent */

    public float blastRadius = 0;

    public void OnCollisionEnter(Collision collision) {
        GameObject particleSystem = Instantiate(hitParticlePrefab, transform.position, transform.rotation);

        if (collision.gameObject.GetComponent<RM_HealthComponent>()) {
            collision.gameObject.GetComponent<RM_HealthComponent>().Damage(damage);
        }
        else if (collision.gameObject.GetComponent<Rigidbody>()) {
            //Blast back any objects without healthcomponent but do have a rigidbody
            collision.gameObject.GetComponent<Rigidbody>().AddForce(GetComponent<Rigidbody>().velocity);
        }
        else {
            //Check for targets in blastradius
            if (blastRadius > 0) {
                Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);

                foreach (Collider c in colliders) {
                    if (c.gameObject.GetComponent<RM_HealthComponent>()) {
                        c.gameObject.GetComponent<RM_HealthComponent>().Damage(damage);
                    }
                }
            }
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
