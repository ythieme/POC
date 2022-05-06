using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public static CheckPointManager instance;
    public Transform lastCheckpoint;
    [SerializeField] Transform player;

    private void Awake()
    {
        instance = this;
    }
    public void OnFall()
    {
        player.position = lastCheckpoint.position;
    }
}
