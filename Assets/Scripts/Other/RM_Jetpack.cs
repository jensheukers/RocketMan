using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RM_Jetpack : MonoBehaviour {
    [SerializeField]
    private float force = 20f;

    [SerializeField]
    private float maxFuel = 200f;

    [SerializeField]
    private float fuelUsage = 10f;

    [SerializeField]
    private float fuelRegenAmount = 5f;

    [SerializeField]
    private float fuel;

    [SerializeField]
    private ParticleSystem boostParticleSystem;

    private void Start() {
        fuel = maxFuel;
    }

    private void Update() {
        AddFuel(fuelRegenAmount * Time.deltaTime);
    }

    public void Boost(GameObject holder) {
        if (fuel <= 0) return;
        holder.GetComponent<Rigidbody>().AddForce(new Vector3(0, force, 0) * Time.deltaTime);

        fuel -= fuelUsage * Time.deltaTime;

        if (boostParticleSystem) boostParticleSystem.Emit(1);
    }

    public void AddFuel(float amount) {
        if (fuel + amount > maxFuel) {
            fuel = maxFuel;
            return;
        }
        fuel += amount;
    }
}
