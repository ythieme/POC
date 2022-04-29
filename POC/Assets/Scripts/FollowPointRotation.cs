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
            followTransform.transform.rotation *= Quaternion.AngleAxis(_look.x * rotationPower, Vector3.up);

            var angles = followTransform.transform.localEulerAngles;
            angles.z = 0;
            angles.x = 30;

            followTransform.transform.localEulerAngles = angles;

        }
        //followTransform.transform.localEulerAngles = new Vector3(angles.x,0,0);
    }
}
