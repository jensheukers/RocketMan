using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEditor.Events;

/// <summary>
/// Main trigger component, has multiple events that can be read and attached to.
/// </summary>
public class RM_Trigger : MonoBehaviour {

    [SerializeField]
    private string triggerKey = "RM_Trigger"; /**Key of the trigger*/

    [SerializeField]
    private KeyCode activateKeyCode = KeyCode.F;

    [SerializeField]
    private bool usingKeycode = false;

    [SerializeField]
    private string activeKeyCodeActionText = "interact";

    [SerializeField]
    private bool destroyOnInteract = false;

    public UnityEvent<Collider> onTriggerEnterEvent; /** OnTriggerEnter action event listener. */
    public UnityEvent<Collider> onTriggerStayEvent; /** OnTriggerStay action event listener. */
    public UnityEvent<Collider> onTriggerExitEvent; /** OnTriggerExit action event listener. */

    public List<string> allowedTags = new List<string> { "RM_Player" };

    //Unity automated events
    private void OnTriggerEnter(Collider other) {
        if (allowedTags.Contains(other.tag)) {
            onTriggerEnterEvent.Invoke(other);

            if (usingKeycode) {
                RM_UIManager um = other.GetComponent<RM_UIManager>();
                if (um) {
                    um.ShowNotification();
                    um.SetNotificationText("Press " + activateKeyCode.ToString() + " to " + activeKeyCodeActionText + ".");
                }
            }
        }
    }

    private void OnTriggerStay(Collider other) {
        if (allowedTags.Contains(other.tag)) {
            if (usingKeycode) {
                if (Input.GetKeyDown(activateKeyCode)) {
                    onTriggerStayEvent.Invoke(other);

                    if (destroyOnInteract) {
                        RM_UIManager um = other.GetComponent<RM_UIManager>();
                        um.HideNotification();
                        Destroy(this.gameObject);
                    }
                }
            }
            else {
                onTriggerStayEvent.Invoke(other);
            }

        }

    }

    private void OnTriggerExit(Collider other) {
        if (allowedTags.Contains(other.tag)) {
            onTriggerExitEvent.Invoke(other);

            if (usingKeycode) {
                RM_UIManager um = other.GetComponent<RM_UIManager>();
                if (um) {
                    um.HideNotification();
                }
            }
        }
    }

    private void ClearPersistantEvents(UnityEvent<Collider> e) {
        Debug.Log(e.GetPersistentEventCount());
        for (int i = e.GetPersistentEventCount() - 1; i > -1 ; i--) {
            UnityEventTools.RemovePersistentListener(e, i);
        }
    }

    /**
     * @brief Returns the trigger name
     * @return string
     */
    public string GetTriggerKey() { return this.triggerKey; }

    /**
     * @brief clears the onTriggerEnter event
     */
    public void ClearOnTriggerEnterEvent() { 
        ClearPersistantEvents(onTriggerEnterEvent);
        onTriggerEnterEvent.RemoveAllListeners(); 
    }

    /**
     * @brief clears the onTriggerStay event
     */
    public void ClearOnTriggerStayEvent() {
        ClearPersistantEvents(onTriggerStayEvent);
        onTriggerStayEvent.RemoveAllListeners();
    }

    /**
     * @brief clears the onTriggerExit event
     */
    public void ClearOnTriggerExitEvent() {
        ClearPersistantEvents(onTriggerExitEvent);
        onTriggerExitEvent.RemoveAllListeners();
    }
}
