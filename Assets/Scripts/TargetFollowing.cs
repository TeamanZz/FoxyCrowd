using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollowing : MonoBehaviour
{
    public Transform foxContainer;
    private Transform foxTransform;

    private void Update()
    {
        if (foxTransform == null)
        {
            int foxesCount = foxContainer.childCount;
            if (foxesCount == 0)
                return;

            if (foxContainer.GetChild(foxesCount - 1) != null)
                foxTransform = foxContainer.GetChild(foxesCount - 1);
        }
        transform.position = Vector3.Lerp(transform.position, new Vector3(0, 0, foxTransform.position.z), 5 * Time.deltaTime);
    }
}