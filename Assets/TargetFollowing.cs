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
            int foxesCount = foxContainer.childCount;
            if (foxContainer.GetChild(foxesCount - 1) != null)
                foxTransform = foxContainer.GetChild(foxesCount - 1);
        }

        transform.position = new Vector3(0, 0, foxTransform.position.z);
    }
}