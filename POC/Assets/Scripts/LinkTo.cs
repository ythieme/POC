using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkTo : MonoBehaviour
{
    [SerializeField]
    Transform linkedTo;
    // Update is called once per frame
    void Update()
    {
        transform.position = linkedTo.position;
    }
}
