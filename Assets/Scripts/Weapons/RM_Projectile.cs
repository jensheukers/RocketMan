using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RM_Projectile : MonoBehaviour {
    [SerializeField]
    private GameObject hitParticlePrefab;

    public void OnCollisionEnter(Collision collision) {
        GameObject particleSystem = Instantiate(hitParticlePrefab, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
