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

    private CinemachineVirtualCamera cinemachineCam;

    [SerializeField]
    private float rotationPower = 1f;

    [Tooltip("Variable cannot be changed at runtime!")]
    [SerializeField] private bool lockCursor = true;

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
        if (!followTarget) return; 

        //Rotate the Follow Target transform based on the input
        followTarget.transform.rotation *= Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationPower, Vector3.up);
        followTarget.transform.rotation *= Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * rotationPower, Vector3.right);

        var angles = followTarget.transform.localEulerAngles;
        angles.z = 0;

        var angle = followTarget.transform.localEulerAngles.x;

        //Clamp the Up/Down rotation
        if (angle > 180 && angle < 340) {
            angles.x = 340;
        }
        else if (angle < 180 && angle > 40) {
            angles.x = 40;
        }


        followTarget.transform.localEulerAngles = angles;

        if (!followObjectRoot) return;
        if (followObjectRoot.GetComponent<RM_CharacterController>() && followObjectRoot.GetComponent<RM_CharacterController>().IsMoving()) {
            //Set the player rotation based on the look transform
            followObjectRoot.rotation = Quaternion.Euler(0, followTarget.transform.rotation.eulerAngles.y, 0);
            //reset the y rotation of the look transform
            followTarget.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
        }
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
