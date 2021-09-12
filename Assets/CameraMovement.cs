using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed;
    private Vector3 velocity = Vector3.zero;
    public float smoothTime = 0.3F;
    private void FixedUpdate()
    {
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}