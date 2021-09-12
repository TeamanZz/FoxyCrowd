using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FoxesCount : MonoBehaviour
{
    [SerializeField] private TextMeshPro foxesCount;
    [SerializeField] private Transform targetTransform;
    private CrowdController crowdController;

    private void Awake()
    {
        crowdController = GameObject.FindObjectOfType<CrowdController>();
    }

    private void FixedUpdate()
    {
        foxesCount.text = crowdController.crowdTransforms.Count.ToString();
        transform.position = new Vector3(targetTransform.position.x, transform.position.y, transform.position.z);
    }
}
