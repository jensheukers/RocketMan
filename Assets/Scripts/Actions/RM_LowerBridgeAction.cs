using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RM_LowerBridgeAction : MonoBehaviour {
    [SerializeField]
    private float desiredOffset = -20;

    [SerializeField]
    private float speed = 1f;

    private bool lowerBridge;

    private Vector3 targetPos;

    private void Start() {
        lowerBridge = false;

        targetPos = transform.localPosition + new Vector3(0, desiredOffset, 0);
    }

    public void LowerBridge() {
        lowerBridge = true;
    }

    private void Update() {
        if (!lowerBridge) return;

        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, speed * Time.deltaTime);
    }
}
