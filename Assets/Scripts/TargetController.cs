using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public Transform fox;

    private void Update()
    {
        var delta = transform.position.z - fox.transform.position.z;
        transform.position = new Vector3(transform.position.x, transform.position.y, fox.position.z + 10);
    }
}