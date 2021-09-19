using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FoxMultiply : MonoBehaviour
{
    private Fox fox;
    private CrowdController crowdController;
    [SerializeField] private TextMeshPro increaseText;

    public int minFoxesIncreaseValue = 1;
    public int maxFoxesIncreaseValue = 10;

    private int foxesIncreaseValue = 0;

    private bool wasCollided = false;

    private void Awake()
    {
        crowdController = GameObject.FindObjectOfType<CrowdController>();
    }

    public int GetFoxesCountIncrease()
    {
        if (foxesIncreaseValue == 0)
        {
            foxesIncreaseValue = Random.Range(minFoxesIncreaseValue, maxFoxesIncreaseValue);
            Debug.Log(foxesIncreaseValue);
            return foxesIncreaseValue;
        }
        else
        {
            Debug.Log(foxesIncreaseValue);
            return foxesIncreaseValue;
        }
    }

    private void Start()
    {
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
