using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

/// <summary>
/// Main trigger component, has multiple events that can be read and attached to.
/// </summary>
public class RM_Trigger : MonoBehaviour {

    [SerializeField]
    private string triggerKey = "RM_Trigger"; /**Key of the trigger*/

    public UnityEvent<Collider> onTriggerEnterEvent; /** OnTriggerEnter action event listener. */
    public UnityEvent<Collider> onTriggerStayEvent; /** OnTriggerStay action event listener. */
    public UnityEvent<Collider> onTriggerExitEvent; /** OnTriggerExit action event listener. */

    public List<string> allowedTags = new List<string> { "RM_Player" };

    //Unity automated events
    private void OnTriggerEnter(Collider other) {
        if (allowedTags.Contains(other.tag)) {
            onTriggerEnterEvent.Invoke(other);
        }
    }

    private void OnTriggerStay(Collider other) {
        if (allowedTags.Contains(other.tag)) {
            onTriggerStayEvent.Invoke(other);
        }

    }

    private void OnTriggerExit(Collider other) {
        if (allowedTags.Contains(other.tag)) {
            onTriggerExitEvent.Invoke(other);
        }
    }

    /**
     * @brief Returns the trigger name
     * @return string
     */
    public string GetTriggerKey() { return this.triggerKey; }
}
