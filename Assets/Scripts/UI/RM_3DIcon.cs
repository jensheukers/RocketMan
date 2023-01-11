using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 3D Icon component class, handles all events with canvas component on this object. 
/// </summary>
public class RM_3DIcon : MonoBehaviour {
    [SerializeField]
    private Transform imageTranform;

    private Canvas canvas;

    private GameObject player;

    private void Start() {
        canvas = GetComponent<Canvas>();

        player = GameObject.FindGameObjectWithTag("RM_Player");

        //Set camera to main camera
        SetCamera(Camera.main);
    }

    private void Update() {
        if (!player) return;

        Quaternion lookRotation = Quaternion.LookRotation(imageTranform.transform.position - player.transform.position, Vector3.up);
        imageTranform.rotation = Quaternion.RotateTowards(imageTranform.rotation, lookRotation, Time.deltaTime * 500);
        imageTranform.localEulerAngles = new Vector3(0, imageTranform.localEulerAngles.y, 0);

    }

    private void SetCamera(Camera camera) {
        if (!canvas) return;
        canvas.worldCamera = camera;
    }
}
