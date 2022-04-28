using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTrigger : MonoBehaviour
{
    public CinemachineVirtualCamera curCam;
    public CinemachineVirtualCamera newCam;

    private void OnTriggerEnter(Collider other)
    {
        curCam = GameManager.instance.currentCam;

        curCam.Priority = 0;
        newCam.Priority = 10;

        newCam = GameManager.instance.currentCam;
    }
}
