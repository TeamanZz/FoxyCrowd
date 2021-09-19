using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItemsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject chickensPrefab;
    [SerializeField] private GameObject portalPrefab;
    [SerializeField] private GameObject endHolePrefab;
    [SerializeField] private Transform interactableThingsContainer;
    [SerializeField] private int minSpawnPointsCount = 1;
    [SerializeField] private int maxSpawnPointsCount = 5;
    [SerializeField] private float startSpawnPointZPosition;
    [SerializeField] private float distanceBetweenInteractions;

    private float lastSpawnPointZPosition;
    private int spawnPointsCount;

    private InteractableItemsSettings spawnPointsSettings;

    private FoxMultiply lastLowPortal;
    private FoxMultiply lastHighPortal;

    private int lastLowPortalIncrease = 0;
    private int lastHighPortalIncrease = 0;

    private void Awake()
    {
        spawnPointsSettings = GetComponent<InteractableItemsSettings>();
        spawnPointsCount = Random.Range(minSpawnPointsCount, maxSpawnPointsCount + 1);
        SpawnInteractablePoints();
    }

    private void SpawnInteractablePoints()
    {
        lastSpawnPointZPosition = startSpawnPointZPosition;
        for (int i = 0; i < spawnPointsCount; i++)
        {
            if (i % 2 == 0)
            {
                var newPortal = Instantiate(portalPrefab, new Vector3(0, 0, lastSpawnPointZPosition), Quaternion.identity);
                newPortal.transform.SetParent(interactableThingsContainer);

                var lowPortalIndex = Random.Range(0, 2);
                var highPortalIndex = 0;
                if (lowPortalIndex == 0)
                    highPortalIndex = 1;

                //Задаём настройки для портала с маленьким значением
                lastLowPortal = newPortal.transform.GetChild(lowPortalIndex).GetChild(0).GetComponent<FoxMultiply>();
                lastLowPortal.minFoxesIncreaseValue = spawnPointsSettings.lowPortalMinValue;
                lastLowPortal.maxFoxesIncreaseValue = spawnPointsSettings.lowPortalMaxValue;

                //Задаём настройки для портала с высоким значением
                lastHighPortal = newPortal.transform.GetChild(highPortalIndex).GetChild(0).GetComponent<FoxMultiply>();
                lastHighPortal.minFoxesIncreaseValue = spawnPointsSettings.lowPortalMaxValue + 10;
                lastHighPortal.maxFoxesIncreaseValue = spawnPointsSettings.highPortalMaxValue;

                lastLowPortalIncrease = lastLowPortal.GetFoxesCountIncrease();
                lastHighPortalIncrease = lastHighPortal.GetFoxesCountIncrease();
            }
            else
            {
                var newChikens = Instantiate(chickensPrefab, new Vector3(0, 0, lastSpawnPointZPosition), Quaternion.identity);
                // Debug.Log(lastLowPortal.GetFoxesCountIncrease() + 3);
                // Debug.Log(lastHighPortal.GetFoxesCountIncrease());
                // Debug.Log(Random.Range(lastLowPortal.GetFoxesCountIncrease() + 3, lastHighPortal.GetFoxesCountIncrease()));
                // Debug.Log("=====================================================");
                newChikens.GetComponent<ChickenSpot>().chickensSpawnCount = Random.Range(lastLowPortalIncrease + 3, lastHighPortalIncrease);

                newChikens.transform.SetParent(interactableThingsContainer);
            }
            lastSpawnPointZPosition += distanceBetweenInteractions;
        }
        var endOfLevel = Instantiate(endHolePrefab, new Vector3(0, 0, lastSpawnPointZPosition + 1), Quaternion.identity);
    }
}