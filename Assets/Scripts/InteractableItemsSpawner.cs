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

    private void Awake()
    {
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
            }
            else
            {
                var newChikens = Instantiate(chickensPrefab, new Vector3(0, 0, lastSpawnPointZPosition), Quaternion.identity);
                newChikens.transform.SetParent(interactableThingsContainer);
            }
            lastSpawnPointZPosition += distanceBetweenInteractions;
        }

        var endOfLevel = Instantiate(endHolePrefab, new Vector3(0, 0, lastSpawnPointZPosition + 1), Quaternion.identity);
    }
}