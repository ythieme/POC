using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FollowPointRotation : MonoBehaviour
{
    public Vector2 _look;
    public GameObject followTransform;
    public float rotationPower;
    public bool camRotate;

    public void OnLook(InputValue value)
    {
        _look = value.Get<Vector2>();
    }

    void Update()
    {
        followRotation();
        LockMouse();
    }

    public void StopRotation()
    {
        camRotate = false;
    }

    public void AllowRotation()
    {
        camRotate = true;
    }

    void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void followRotation()
    {
        if (camRotate)
        {
            followTransform.transform.rotation *= Quaternion.AngleAxis(_look.x * rotationPower, Vector3.up);
            followTransform.transform.rotation *= Quaternion.AngleAxis(_look.y * rotationPower, Vector3.right);

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
}
