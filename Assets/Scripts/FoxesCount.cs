using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FoxesCount : MonoBehaviour
{
    [SerializeField] private GameObject howMuchFoxesAdded;
    [SerializeField] private TextMeshPro foxesCount;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private float lerpSpeed = 5;
    private CrowdController crowdController;

    private void Awake()
    {
        crowdController = GameObject.FindObjectOfType<CrowdController>();
    }

    private void FixedUpdate()
    {
        var thisPos = Camera.main.WorldToScreenPoint(transform.position);
        howMuchFoxesAdded.transform.position = new Vector3(thisPos.x, howMuchFoxesAdded.transform.position.y, howMuchFoxesAdded.transform.position.z);
        foxesCount.text = crowdController.crowdContainer.childCount.ToString();
        var toPos = new Vector3(targetTransform.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, toPos, lerpSpeed * Time.deltaTime);
    }
}
