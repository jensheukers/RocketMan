using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RM_Jetpack : MonoBehaviour {
    [SerializeField]
    private float force = 20f; /** Amount of force on the y velocity*/

    [SerializeField]
    private float maxFuel = 200f; /** maximum fuel capacity*/

    [SerializeField]
    private float fuelUsage = 10f; /**Fuel usage per frame*/

    [SerializeField]
    private float fuelRegenAmount = 5f; /**Amount of fuel regeneration per frame*/

    [SerializeField]
    private float fuel; /**Amount of fuel*/

    [SerializeField]
    private ParticleSystem smokeParticleSystem; /** Patricle system refernce for smoke */

    [SerializeField]
    private ParticleSystem trailParticleSystem; /** Patricle system refernce for trail */

    private AudioSource _audioSource; /** Reference to audio source*/

    private void Start() {
        fuel = maxFuel;

        _audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        AddFuel(fuelRegenAmount * Time.deltaTime);
    }

    /**
     * @brief Applies upwards motion also handles particle systems
     * @param GameObject holder game object
     */
    public void Boost(GameObject holder) {
        if (_audioSource) {
            if (!_audioSource.isPlaying) _audioSource.Play();
        }
        if (fuel <= 0) return;
        holder.GetComponent<Rigidbody>().AddForce(new Vector3(0, force, 0) * Time.deltaTime);

        fuel -= fuelUsage * Time.deltaTime;
        if (smokeParticleSystem) {
            if (!smokeParticleSystem.isPlaying) {
                smokeParticleSystem.Play();
            }
        }

        if (trailParticleSystem) {
            if (!trailParticleSystem.isPlaying) {
                trailParticleSystem.Play();
            }
        }
    }

    /**
     * @brief Adds fuel to the jetpack
     * @param float
     */
    public void AddFuel(float amount) {
        if (fuel + amount > maxFuel) {
            fuel = maxFuel;
            return;
        }
        fuel += amount;
    }

    /*
     * @brief Returns the current fuel
     */
    public float GetFuel() {
        return fuel;
    }

    /*
     * @brief Returns the max fuel
     */
    public float GetMaxFuel() {
        return maxFuel;
    }
}
