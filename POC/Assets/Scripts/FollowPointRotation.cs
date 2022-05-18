using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FollowPointRotation : MonoBehaviour
{
    public Vector2 _look;
    public GameObject followTransform;
    public float rotationPowerHorizontal;
    public float rotationPowerVertical;
    public bool camRotate;
    public bool inverted;

    public void OnLook(InputValue value)
    {
        _look = value.Get<Vector2>();
    }

    void Update()
    {
        followRotation();
    }

    public void StopRotation()
    {
        camRotate = false;
    }

    public void AllowRotation()
    {
        camRotate = true;
    }
    void followRotation()
    {
        if (camRotate)
        {
            if (inverted)
            {
                followTransform.transform.rotation *= Quaternion.AngleAxis(_look.x * rotationPowerHorizontal, Vector3.up);
                followTransform.transform.rotation *= Quaternion.AngleAxis(_look.y * -rotationPowerVertical, Vector3.right);
                //followTransform.transform.rotation *= Quaternion.AngleAxis(inverted ? _look.x : -_look.y * rotationPowerHorizontal, Vector3.up)
            }

            else if (!inverted)
            {
                followTransform.transform.rotation *= Quaternion.AngleAxis(_look.x * rotationPowerHorizontal, Vector3.up);
                followTransform.transform.rotation *= Quaternion.AngleAxis(_look.y * rotationPowerVertical, Vector3.right);
            }
            var angles = followTransform.transform.localEulerAngles;
            angles.z = 0;

            var angle = followTransform.transform.localEulerAngles.x;

            if (angle > 180 && angle < 355)
            {
                angles.x = 355;
            }
            else if (angle < 180 && angle > 40)
            {
                angles.x = 40;
            }

            followTransform.transform.localEulerAngles = angles;

        }
    }

    void YaxisInverted()
    {

    }
}
