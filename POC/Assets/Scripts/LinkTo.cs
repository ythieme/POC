using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkTo : MonoBehaviour
{

    [SerializeField]
    Vector3 offset;

    [SerializeField]
    Transform linkedTo;

    bool resetx = false;

    // Update is called once per frame
    void Update()
    {
        transform.position = linkedTo.position + offset;
    }

    private void ResetXOffset()
    {
        if (resetx && offset.x != 0)
        {

        }
    }
}
