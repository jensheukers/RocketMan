using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Animations.Rigging;

public class RM_AimStateManager : MonoBehaviour {
    private float aimAmount;

    [SerializeField]
    private float aimSpeed = 5f;

    [SerializeField]
    private float zoomSpeed = 50f;

    [SerializeField]
    private float minFov = 40f;

    [SerializeField]
    private Transform aimTarget;

    [SerializeField]
    private float aimSmoothSpeed = 20;

    [SerializeField]
    private LayerMask aimMask;

    Animator animator;

    CinemachineVirtualCamera virtualCamera;
    float originalFov = 0;

    private void Start() {
        GameObject cam = GameObject.FindGameObjectWithTag("CinemachineCam");
        if (cam) {
            virtualCamera = cam.GetComponent<CinemachineVirtualCamera>();
            originalFov = virtualCamera.m_Lens.FieldOfView;
        }

        if (!aimTarget) {
            Debug.LogError("RM_AimStateManager: aimTarget not set up");
            return;
        }
    }

    private void LateUpdate() {
        if (Input.GetButton("Zoom")) {
            if (aimAmount < 1) aimAmount += aimSpeed * Time.deltaTime;
            else aimAmount = 1;

            if (virtualCamera.m_Lens.FieldOfView > minFov) {
                virtualCamera.m_Lens.FieldOfView -= zoomSpeed * Time.deltaTime;
            }
            else virtualCamera.m_Lens.FieldOfView = minFov;
        }
        else {
            aimAmount = 0;

            if (virtualCamera.m_Lens.FieldOfView < originalFov) {
                virtualCamera.m_Lens.FieldOfView += zoomSpeed * Time.deltaTime;
            }
            else virtualCamera.m_Lens.FieldOfView = originalFov;
        }

        //if (GetComponent<RM_CharacterController>().IsMoving() || aimAmount > 0) {
            Vector2 screenCentre = new Vector2(Screen.width / 2, Screen.height / 2);
            Ray ray = Camera.main.ScreenPointToRay(screenCentre);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimMask)) {
                aimTarget.position = Vector3.Lerp(aimTarget.position, hit.point, aimSmoothSpeed * Time.deltaTime);
            }
        //}
    }

    public float GetAimAmount() {
        return this.aimAmount;
    }
}
