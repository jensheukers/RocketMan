using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RM_Projectile : MonoBehaviour {
    public void OnCollisionEnter(Collision collision) {
        Destroy(gameObject);
    }
}
