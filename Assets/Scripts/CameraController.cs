using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Transform finish;
    

    public bool followTarget = false;

    void Update()
    {
        if (followTarget)
        {
            transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
        }
    }

    public IEnumerator MoveCameraFromFinishToPlayer()
    {
        followTarget = false;
        float cameraSpeed = 0.2f;
        transform.position = new Vector3(finish.position.x, transform.position.y, transform.position.z);

        while (transform.position.x - target.position.x > cameraSpeed)
        {
            transform.position = new Vector3(transform.position.x - cameraSpeed, transform.position.y, transform.position.z);
            yield return null;
        }

        followTarget = true;
    }
}