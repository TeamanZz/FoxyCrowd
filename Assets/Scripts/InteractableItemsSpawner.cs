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
    public float startSpawnPointZPosition;
    [SerializeField] private float distanceBetweenInteractions;

    private float lastSpawnPointZPosition;
    private int spawnPointsCount;

    private InteractableItemsSettings spawnPointsSettings;

    private PortalPiece portalWithLowValue;
    private PortalPiece potalWithHighValue;

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
        SetStartSpawnPointPosition();
        for (int i = 0; i < spawnPointsCount; i++)
        {
            if (i % 2 == 0 && !CheckOnPortalSkip())
            {
                SpawnMultiplyPortal();
            }
            else
            {
                SpawnThreat();
            }
            SetNewSpawnPointPosition();
        }
        SpawnEndOfLevel();
    }

    private void SetStartSpawnPointPosition()
    {
        lastSpawnPointZPosition = startSpawnPointZPosition;
    }

    private void SetNewSpawnPointPosition()
    {
        lastSpawnPointZPosition += distanceBetweenInteractions;
    }

    private void SpawnThreat()
    {
        ThreatType threatType = SetThreatType();

        if (threatType == ThreatType.Trap)
        {
            SpawnTrap();
        }
        else if (threatType == ThreatType.ChickenSpot)
        {
            SpawnChickenSpot();
        }
    }

    private bool CheckOnPortalSkip()
    {
        int portalSkipNumber = Random.Range(0, portalSkipChanse);

        return (portalSkipNumber == 0);
    }

    private void SpawnMultiplyPortal()
    {
        var newPortal = Instantiate(portalPrefab, new Vector3(0, 0, lastSpawnPointZPosition), Quaternion.identity);
        newPortal.transform.SetParent(interactableThingsContainer);

        var lowPortalIndex = Random.Range(0, 2);
        var highPortalIndex = 0;
        if (lowPortalIndex == 0)
            highPortalIndex = 1;

        //Задаём настройки для портала с маленьким значением: SpawnValue = Random.Range(lowPortalMinValue,lowPortalMaxValue)
        portalWithLowValue = newPortal.transform.GetChild(lowPortalIndex).GetChild(0).GetComponent<PortalPiece>();
        portalWithLowValue.minFoxesIncreaseValue = spawnPointsSettings.lowPortalMinValue;
        portalWithLowValue.maxFoxesIncreaseValue = spawnPointsSettings.lowPortalMaxValue;

        //Задаём настройки для портала с высоким значением
        potalWithHighValue = newPortal.transform.GetChild(highPortalIndex).GetChild(0).GetComponent<PortalPiece>();
        potalWithHighValue.minFoxesIncreaseValue = spawnPointsSettings.lowPortalMaxValue + 10;
        potalWithHighValue.maxFoxesIncreaseValue = spawnPointsSettings.highPortalMaxValue;

        lastLowPortalIncrease = portalWithLowValue.GetFoxesCountIncrease();
        lastHighPortalIncrease = potalWithHighValue.GetFoxesCountIncrease();
    }

    private void SpawnEndOfLevel()
    {
        var endOfLevel = Instantiate(endHolePrefab, new Vector3(0, 0, lastSpawnPointZPosition + 1), Quaternion.identity);
    }

    private void SpawnChickenSpot()
    {
        var newChikensSpot = Instantiate(chickensPrefab, new Vector3(0, 0, lastSpawnPointZPosition), Quaternion.identity);
        SetChickensCountInSpawnPoint(newChikensSpot);

        newChikensSpot.transform.SetParent(interactableThingsContainer);
    }

    private void SpawnTrap()
    {
        int trapType = Random.Range(0, traps.Count);

        var newTrap = Instantiate(traps[trapType], new Vector3(Random.Range(-1.6f, 1.6f), 0, lastSpawnPointZPosition - 1), Quaternion.identity);
        newTrap.transform.SetParent(interactableThingsContainer);
    }

    private ThreatType SetThreatType()
    {
        int threatsCount = System.Enum.GetNames(typeof(ThreatType)).Length;
        ThreatType threatType = (ThreatType)Random.Range(0, threatsCount);
        return threatType;
    }

    private enum ThreatType
    {
        ChickenSpot,
        Trap
    }

    private void SetChickensCountInSpawnPoint(GameObject newChikens)
    {
        newChikens.GetComponent<ChickenSpot>().chickensSpawnCount = Random.Range(lastLowPortalIncrease + 3, lastHighPortalIncrease);
    }

    public float GetWayDistance(float startPosition)
    {
        float distance = Mathf.Abs(lastSpawnPointZPosition - startPosition);
        return distance;
    }
}