using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Player player;
    public Transform target;
    [NonSerialized]
    Vector3 offset;
    Vector3 CameraCurrPos;

    private void Start()
    {
        offset = transform.position - target.position;
        CameraCurrPos = transform.position;
    }
    void Update()
    {
        if(!player.isResultDeclare)
        {
            //transform.position = target.position + offset;
            CameraCurrPos.z = target.position.z + offset.z;
            transform.position = CameraCurrPos;
        }
       
    }
}
