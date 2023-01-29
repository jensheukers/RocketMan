using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RM_LerpMoveObjectAction : MonoBehaviour {
    [SerializeField]
    private Vector3 desiredOffset = new Vector3(0,-20, 0);

    [SerializeField]
    private float speed = 1f;

    private bool move;

    private Vector3 targetPos;

    private void Start() {
        move = false;

        targetPos = transform.localPosition + desiredOffset;
    }

    public void Move() {
        move = true;
    }

    private void Update() {
        if (!move) return;

        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, speed * Time.deltaTime);
    }
}
