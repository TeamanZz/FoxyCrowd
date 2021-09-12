using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBMovement : MonoBehaviour
{
    public Rigidbody foxesRigidbody;
    public float moveSpeed;

    private void FixedUpdate()
    {
        Vector3 movementVector = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, 0, 0);

        foxesRigidbody.MovePosition(movementVector);
    }
}
