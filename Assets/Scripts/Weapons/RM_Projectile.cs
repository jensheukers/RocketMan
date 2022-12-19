using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RM_Projectile : MonoBehaviour {
    [SerializeField]
    private ParticleSystem explosionParticleSystem;

    public void OnCollisionEnter(Collision collision) {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().useGravity = false;

        explosionParticleSystem.Play();
        Destroy(gameObject, explosionParticleSystem.main.duration);
    }
}
