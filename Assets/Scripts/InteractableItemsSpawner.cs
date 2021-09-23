using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItemsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject chickensPrefab;
    [SerializeField] private List<GameObject> traps;
    [SerializeField] private GameObject portalPrefab;
    [SerializeField] private GameObject endHolePrefab;
    [SerializeField] private Transform interactableThingsContainer;
    [Range(0, 30)]
    [SerializeField] private int portalSkipChanse = 30;
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
            bool isPortalSkip = false;
            int portalSkipNumber = Random.Range(0, portalSkipChanse);
            if (portalSkipNumber == 0)
                isPortalSkip = true;

            if (i % 2 == 0 && !isPortalSkip)
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
                int dangerousType = Random.Range(0, 2);
                if (dangerousType == 0)
                {
                    int trapType = Random.Range(0, traps.Count);

                    var newTrap = Instantiate(traps[trapType], new Vector3(Random.Range(-1.6f, 1.6f), 0, lastSpawnPointZPosition - 1), Quaternion.identity);
                    newTrap.transform.SetParent(interactableThingsContainer);
                }
                else
                {
                    var newChikens = Instantiate(chickensPrefab, new Vector3(0, 0, lastSpawnPointZPosition), Quaternion.identity);
                    newChikens.GetComponent<ChickenSpot>().chickensSpawnCount = Random.Range(lastLowPortalIncrease + 3, lastHighPortalIncrease);

                    newChikens.transform.SetParent(interactableThingsContainer);
                }
            }
            lastSpawnPointZPosition += distanceBetweenInteractions;
        }
        var endOfLevel = Instantiate(endHolePrefab, new Vector3(0, 0, lastSpawnPointZPosition + 1), Quaternion.identity);
    }
}