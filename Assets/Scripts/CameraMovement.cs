using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform cameraTarget;
    public float offset;

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, cameraTarget.position.z - offset);
    }
}