using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FoxMultiply : MonoBehaviour
{
    private Fox fox;
    private CrowdController crowdController;
    [SerializeField] private TextMeshPro increaseText;

    [SerializeField] private int minFoxesIncreaseValue = 1;
    [SerializeField] private int maxFoxesIncreaseValue = 10;

    private int foxesIncreaseValue;

    private bool wasCollided = false;

    private void Awake()
    {
        crowdController = GameObject.FindObjectOfType<CrowdController>();
    }

    private void Start()
    {
        foxesIncreaseValue = Random.Range(minFoxesIncreaseValue, maxFoxesIncreaseValue);
        increaseText.text = "+" + foxesIncreaseValue.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Fox>(out fox))
        {
            if (wasCollided)
                return;
            crowdController.SpawnFoxes(foxesIncreaseValue);
            crowdController.ChangeObstacleAvoidanceRadius();
            Destroy(transform.parent.parent.gameObject);
            wasCollided = true;
        }
    }
}
