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
    private int foxesXIncreaseValue = 1;

    private MultiplyPortal multiplyPortal;

    private bool isXType = false;

    private void Awake()
    {
        var num = Random.Range(0, 4);

        if (num == 0)
            isXType = true;

        crowdController = GameObject.FindObjectOfType<CrowdController>();
        multiplyPortal = transform.parent.parent.GetComponent<MultiplyPortal>();
    }

    public int GetFoxesCountIncrease()
    {
        if (isXType)
        {
            if (foxesXIncreaseValue == 1)
            {
                foxesXIncreaseValue = Random.Range(2, 4);
            }
        }

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
        if (!isXType)
            increaseText.text = "+" + foxesIncreaseValue.ToString();
        else
            increaseText.text = "x" + foxesXIncreaseValue.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Fox>(out fox))
        {
            if (multiplyPortal.wasCollided)
                return;

            SFXHandler.sFXHandler.PlayFoxMultiply();

            crowdController.SpawnFoxes(foxesIncreaseValue, foxesXIncreaseValue);
            crowdController.ChangeObstacleAvoidanceRadius();
            Destroy(transform.parent.parent.gameObject);
            multiplyPortal.wasCollided = true;
        }
    }
}