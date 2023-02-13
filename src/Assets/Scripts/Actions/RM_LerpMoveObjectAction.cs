using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Lerps a object to desiredOffset when Move() is called
/// </summary>
public class RM_LerpMoveObjectAction : MonoBehaviour {
    [SerializeField]
    private Vector3 desiredOffset = new Vector3(0,-20, 0); /** The desired offset*/

    [SerializeField]
    private float speed = 1f; /** The speed at what to move the object at*/

    private bool move; /**If ture object will move*/

    private Vector3 targetPos; /** The target position*/

    private void Start() {
        move = false;

        targetPos = transform.localPosition + desiredOffset;
    }

    /**
     * Sets move bool to true
     */
    public void Move() {
        move = true;
    }

    private void Update() {
        if (!move) return;

        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, speed * Time.deltaTime);
    }
}
