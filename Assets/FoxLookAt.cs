using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxLookAt : MonoBehaviour
{
    private bool needLookAtSpecificPoint = false;
    private Vector3 specificPoint;

    private void FixedUpdate()
    {
        transform.LookAt(new Vector3(transform.position.x, 0, transform.position.z + 1), Vector3.left);
    }

    public void SetTarget(Vector3 lookAt)
    {
        specificPoint = lookAt;
        needLookAtSpecificPoint = true;
    }

    public void RemoveTarget()
    {
        needLookAtSpecificPoint = false;

    }
}
