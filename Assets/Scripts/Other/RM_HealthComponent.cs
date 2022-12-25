using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RM_HealthComponent : MonoBehaviour {
    [SerializeField]
    private int maxHealth = 100; /*** The maximum health this entity can have*/
    private int currentHealth; /*** The current health of the entity*/

    [SerializeField]
    private bool destroyOnHealthZero;

    public UnityEvent onHealthZeroEvent; /** OnHealthZero action event listener. */

    private void Awake() {
        currentHealth = maxHealth;
    }

    /*
     * @brief Add amount to currentHealth
     * @param int
     */
    public void Heal(int amount) {
        if (currentHealth + amount > maxHealth) currentHealth = maxHealth;
        else currentHealth += amount;
    }

    /*
     * @brief Removes amount from currentHealth, then invokes onHealthZeroEvent
     * @param int
     */
    public void Damage(int amount) {
        Debug.Log(amount);
        currentHealth -= amount;

        if (currentHealth < 0) {
            onHealthZeroEvent.Invoke();

            if (destroyOnHealthZero) {
                Destroy(gameObject);
            }
        }
    }

    /*
     * @brief Returns the current health
     */
    public int GetHealth() {
        return currentHealth;
    }

    /*
     * @brief Returns the max health
     */
    public int GetMaxHealth() {
        return maxHealth;
    }
}
