using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RM_Jetpack : MonoBehaviour {
    [SerializeField]
    private float force = 20f;

    public void Boost(GameObject holder) {
        holder.GetComponent<Rigidbody>().AddForce(new Vector3(0, force, 0) * Time.deltaTime);
    }
}
