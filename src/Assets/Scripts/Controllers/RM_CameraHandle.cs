using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/// <summary>
/// RM_CameraHandle handles all camera movement by rotating a followtarget, however if followObjectRoot is set, it will rotate that object based on the horizontal rotation of mouse input.
/// </summary>
public class RM_CameraHandle : MonoBehaviour {

    [SerializeField]
    private Transform followTarget; /** The follow transform target*/

    [SerializeField]
    private Transform followObjectRoot; /** The follow root object (For example player object)*/

    private CinemachineVirtualCamera cinemachineCam; /** The cinemachine camera object*/

    [SerializeField]
    private float rotationPower = 1f; /**Rotation power */

    [SerializeField]
    private float minCameraClamp = 50; /**Min camera clamp*/

    [SerializeField]
    private float maxCameraClamp = 330; /** Max camera clamp*/


    [Tooltip("Variable cannot be changed at runtime!")]
    [SerializeField] private bool lockCursor = true; /**Locks the cursor*/

    private void Start() {
        if (lockCursor) Cursor.lockState = CursorLockMode.Locked;

        //Search the cinemachine camera then set its follow target to our follow target
        cinemachineCam = GameObject.FindGameObjectWithTag("CinemachineCam").GetComponent<CinemachineVirtualCamera>();

        if (!cinemachineCam) {
            Debug.LogError("No cinemachine virtual camera in scene, or tag is not correctly set up");
        }
        else {
            cinemachineCam.Follow = followTarget;
        }
    }

    private void Update() {
        if (Time.timeScale == 0) return;
        if (!followTarget) return; 

        //Rotate the Follow Target transform based on the input
        followTarget.transform.rotation *= Quaternion.AngleAxis(Input.GetAxis("CameraHorizontal") * rotationPower, Vector3.up);
        followTarget.transform.rotation *= Quaternion.AngleAxis(Input.GetAxis("CameraVertical") * rotationPower, Vector3.left);

        Vector3 angles = followTarget.transform.localEulerAngles;
        angles.z = 0;

        float angle = followTarget.transform.localEulerAngles.x;

        //Clamp the Up/Down rotation
        if (angle > 180 && angle < maxCameraClamp) {
            angles.x = maxCameraClamp;
        }
        else if (angle < 180 && angle > minCameraClamp) {
            angles.x = minCameraClamp;
        }


        followTarget.transform.localEulerAngles = angles;

        if (!followObjectRoot) return;
        //if (followObjectRoot.GetComponent<RM_CharacterController>() && followObjectRoot.GetComponent<RM_CharacterController>().IsMoving() || followObjectRoot.GetComponent<RM_AimStateManager>().GetAimAmount() > 0) {
            RotateFollowObject(angles);
        //}
    }

    /**
     * Rotates around the follow object
     * @param Vector3
     */
    private void RotateFollowObject(Vector3 angles) {
        //Set the player rotation based on the look transform
        followObjectRoot.rotation = Quaternion.Euler(0, followTarget.transform.rotation.eulerAngles.y, 0);
        //reset the y rotation of the look transform
        followTarget.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
    }

    /**
     * @brief Sets the followTarget
     * @param Transform
     */
    public void SetFollowTarget(Transform target) {
        this.followTarget = target;

        if (cinemachineCam) cinemachineCam.Follow = target;
    }

    /**
     * @brief Sets the followObjectRoot
     */
    public void SetFollowObjectRoot(Transform target) {
        this.followObjectRoot = target;
    }
}
