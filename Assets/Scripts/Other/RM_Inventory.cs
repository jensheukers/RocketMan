using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Very simple inventory system, so we can handle certain pickups by storing them as strings
/// </summary>
public class RM_Inventory : MonoBehaviour {
    private List<string> items;

    private void Start() {
        items = new List<string>();
    }

    public void AddItem(RM_PickupSO pickup) {
        AddItem(pickup.GetType().Name);
    }

    public void AddItem(string itemName) {
        items.Add(itemName);
    }

    public bool HasItem(string itemName) {
        for (int i = 0; i < items.Count; i++) {
            if (items[i] == itemName) {
                return true;
            }
        }

        return false;
    }

    public bool HasItem(RM_PickupSO pickup) {
        return HasItem(pickup.GetType().Name);
    }

    public void RemoveItem(string itemName) {
        for (int i = 0; i < items.Count; i++) {
            if (items[i] == itemName) {
                items.RemoveAt(i);
            }
        }
    }

    public void RemoveItem(RM_PickupSO pickup) {
        RemoveItem(pickup.GetType().Name);
    }
}
