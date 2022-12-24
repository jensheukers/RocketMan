using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RM_Projectile : MonoBehaviour {
    [SerializeField]
    private GameObject hitParticlePrefab;

    private int damage; 

    public void OnCollisionEnter(Collision collision) {
        GameObject particleSystem = Instantiate(hitParticlePrefab, transform.position, transform.rotation);

        if (collision.gameObject.GetComponent<RM_HealthComponent>()) {
            collision.gameObject.GetComponent<RM_HealthComponent>().Damage(damage);
        }

        Destroy(this.gameObject);
    }

    public void SetDamage(int amount) {
        damage = amount;
    }
}
