using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenSpot : MonoBehaviour
{
    public GameObject chickenPrefab;
    [HideInInspector] public bool fightState = false;
    private CrowdController crowdController;

    private float distanceToNearestEnemy = 0;
    private float distanceToFight = 2;

    private void Awake()
    {
        crowdController = GameObject.FindObjectOfType<CrowdController>();
    }

    private void GetDistanceToNearestEnemy()
    {
        float minDistance = Mathf.Infinity;

        for (int i = 0; i < crowdController.crowdTransforms.Count; i++)
        {
            var distance = Vector3.Distance(transform.position, crowdController.crowdTransforms[i].position);
            if (distance < minDistance)
            {
                minDistance = distance;
            }
        }
        distanceToNearestEnemy = minDistance;

        if (distanceToNearestEnemy < distanceToFight && fightState == false)
        {
            crowdController.ReduceCrowdSpeed();
            fightState = true;
        }
    }

    private void Start()
    {
        SpawnChickens();
        InvokeRepeating("GetDistanceToNearestEnemy", 0, 0.5f);
    }

    public void SpawnChickens()
    {
        int chickensCount = Random.Range(10, 30);
        for (int i = 0; i < chickensCount; i++)
        {
            var newChicken = Instantiate(chickenPrefab, transform);
            newChicken.GetComponent<Chicken>().chickenSpot = this;

            if (i == chickensCount - 1)
            {
                newChicken.GetComponent<Chicken>().lastEnemy = true;
            }
        }
    }
}