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

    private MultiplyPortal multiplyPortal;

    private void Awake()
    {
        crowdController = GameObject.FindObjectOfType<CrowdController>();
        multiplyPortal = transform.parent.parent.GetComponent<MultiplyPortal>();
    }

    public int GetFoxesCountIncrease()
    {
        if (foxesIncreaseValue == 0)
        {
            foxesIncreaseValue = Random.Range(minFoxesIncreaseValue, maxFoxesIncreaseValue);
            return foxesIncreaseValue;
        }
        else
        {
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
            if (multiplyPortal.wasCollided)
                return;

            SFXHandler.sFXHandler.PlayFoxMultiply();
            crowdController.SpawnFoxes(foxesIncreaseValue);
            crowdController.ChangeObstacleAvoidanceRadius();
            Destroy(transform.parent.parent.gameObject);
            multiplyPortal.wasCollided = true;
        }
    }
}