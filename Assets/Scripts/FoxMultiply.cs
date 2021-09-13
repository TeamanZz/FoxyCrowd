using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FoxMultiply : MonoBehaviour
{
    private Fox fox;
    private CrowdController crowdController;
    [SerializeField] TextMeshPro increaseText;

    private int foxesIncreaseValue;

    private bool wasCollided = false;

    private void Awake()
    {
        crowdController = GameObject.FindObjectOfType<CrowdController>();
    }

    private void Start()
    {
        foxesIncreaseValue = Random.Range(10, 50);
        increaseText.text = "+" + foxesIncreaseValue.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Fox>(out fox))
        {
            if (wasCollided)
                return;
            crowdController.SpawnFoxes(foxesIncreaseValue);
            Destroy(transform.parent.parent.gameObject);
            wasCollided = true;
        }
    }
}
