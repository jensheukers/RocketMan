using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Very simple inventory system, so we can handle certain pickups by storing them as strings
/// </summary>
public class RM_Inventory : MonoBehaviour {
    private List<string> items; /**List of item names*/

    private void Start() {
        items = new List<string>();
    }

    /**
     * @brief Adds a item to items list
     * @param RM_PickupSO
     */
    public void AddItem(RM_PickupSO pickup) {
        AddItem(pickup.GetType().Name);
    }

    /**
     * @brief Adds a item to items list
     * @param string
     */
    public void AddItem(string itemName) {
        items.Add(itemName);
    }

    /**
     * @brief Checks if player has item
     * @param string
     * @return bool
     */
    public bool HasItem(string itemName) {
        for (int i = 0; i < items.Count; i++) {
            if (items[i] == itemName) {
                return true;
            }
        }

        return false;
    }

    /**
     * @brief Checks if player has item
     * @param RM_PickupSO
     * @return bool
     */
    public bool HasItem(RM_PickupSO pickup) {
        return HasItem(pickup.GetType().Name);
    }

    /**
     * @brief Removes item from items list
     * @param string
     */
    public void RemoveItem(string itemName) {
        for (int i = 0; i < items.Count; i++) {
            if (items[i] == itemName) {
                items.RemoveAt(i);
            }
        }
    }

    /**
     * @brief Removes item from items list
     * @param RM_PickupSO
     */
    public void RemoveItem(RM_PickupSO pickup) {
        RemoveItem(pickup.GetType().Name);
    }
}
