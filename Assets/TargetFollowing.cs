using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollowing : MonoBehaviour
{
    private Transform foxTransform;
    public Transform foxContainer;

    private void Update()
    {
        if (foxTransform == null)
        {
            if (foxContainer.GetChild(0) != null)
                foxTransform = foxContainer.GetChild(0);
        }

        transform.position = new Vector3(0, 0, foxTransform.position.z);
    }
}