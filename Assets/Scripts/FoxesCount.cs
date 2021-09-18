using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FoxesCount : MonoBehaviour
{
    [SerializeField] private TextMeshPro foxesCount;
    [SerializeField] private Transform targetTransform;
    private CrowdController crowdController;
    [SerializeField] private float lerpSpeed = 5;

    private void Awake()
    {
        crowdController = GameObject.FindObjectOfType<CrowdController>();
    }

    private void FixedUpdate()
    {
        foxesCount.text = crowdController.crowdTransforms.Count.ToString();
        var toPos = new Vector3(targetTransform.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, toPos, lerpSpeed * Time.deltaTime);
    }
}
