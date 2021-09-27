using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PortalPiece : MonoBehaviour
{
    private Fox fox;
    private CrowdController crowdController;
    [SerializeField] private TextMeshPro increaseText;

    public int minFoxesIncreaseValue = 1;
    public int maxFoxesIncreaseValue = 10;

    private int foxesIncreaseValue = 0;
    private int foxesMultiplicationValue = 1;

    private MultiplyPortal parentPortal;

    private bool isMultiplication = false;
    private int multiplicationChanse = 3;

    private void Awake()
    {
        SetPortalPieceType();

        crowdController = GameObject.FindObjectOfType<CrowdController>();
        parentPortal = transform.parent.parent.GetComponent<MultiplyPortal>();
    }

    private void SetPortalPieceType()
    {
        var num = Random.Range(0, multiplicationChanse + 1);
        if (num == 0)
            isMultiplication = true;
    }

    public int GetFoxesCountIncrease()
    {
        if (isMultiplication)
        {
            if (foxesMultiplicationValue == 1)
            {
                foxesMultiplicationValue = Random.Range(2, 4);
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
        if (!isMultiplication)
            increaseText.text = "+" + foxesIncreaseValue.ToString();
        else
            increaseText.text = "x" + foxesMultiplicationValue.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Fox>(out fox))
        {
            if (parentPortal.wasCollided)
                return;

            SFXHandler.sFXHandler.PlayFoxMultiply();

            crowdController.SpawnFoxes(foxesIncreaseValue, foxesMultiplicationValue);
            crowdController.ChangeObstacleAvoidanceRadius();
            Destroy(transform.parent.parent.gameObject);
            parentPortal.wasCollided = true;
        }
    }
}