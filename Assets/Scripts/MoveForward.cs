using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed;

    private void FixedUpdate()
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed);
        transform.position = Vector3.Lerp(transform.position, newPosition, 0.01f);
    }
}