using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RM_CameraRotate : MonoBehaviour {

    [SerializeField]
    private Transform followTarget; /** The follow transform target*/

    [SerializeField]
    private Transform followObjectRoot; /** The follow root object (For example player object)*/

    [SerializeField]
    private float rotationPower = 1f;

    private Vector3 nextPosition;
    private Quaternion nextRotation;

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
    
}
