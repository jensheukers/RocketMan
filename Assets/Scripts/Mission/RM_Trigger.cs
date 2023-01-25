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

    [SerializeField]
    private KeyCode activateKeyCode = KeyCode.F;

    [SerializeField]
    private bool usingKeycode = false;

    [SerializeField]
    private string activeKeyCodeActionText = "interact";

    [SerializeField]
    private bool destroyOnInteract = false;

    [SerializeField]
    private bool triggerOnce = false;
    private bool triggeredOnce;

    public UnityEvent<Collider> onTriggerEnterEvent; /** OnTriggerEnter action event listener. */
    public UnityEvent<Collider> onTriggerStayEvent; /** OnTriggerStay action event listener. */
    public UnityEvent<Collider> onTriggerExitEvent; /** OnTriggerExit action event listener. */

    public List<string> allowedTags = new List<string> { "RM_Player" };

    private void Start() {
        triggeredOnce = false;
    }

    //Unity automated events
    private void OnTriggerEnter(Collider other) {
        if (triggeredOnce && triggerOnce) return;

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
        if (triggeredOnce && triggerOnce) return;
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
        if (triggeredOnce && triggerOnce) return;
        if (allowedTags.Contains(other.tag)) {
            onTriggerExitEvent.Invoke(other);

            if (usingKeycode) {
                RM_UIManager um = other.GetComponent<RM_UIManager>();
                if (um) {
                    um.HideNotification();
                }
            }

            triggeredOnce = true;
        }
    }
    /**
     * @brief Returns the trigger name
     * @return string
     */
    public string GetTriggerKey() { return this.triggerKey; }
}
