using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChickenSpot : MonoBehaviour
{
    public GameObject chickenPrefab;
    [HideInInspector] public int chickensSpawnCount;

    [HideInInspector] public bool isFighting = false;
    [HideInInspector] public bool fightEnded = false;

    [SerializeField] private float distanceToStartFight = 2;

    private CrowdController crowdController;

    private void Start()
    {
        crowdController = GameObject.FindObjectOfType<CrowdController>();
        SpawnChickens();
        InvokeRepeating("CheckOnStartFight", 1, 0.5f);
        InvokeRepeating("CheckOnEndFight", 1, 0.5f);
    }

    private void CheckOnEndFight()
    {
        if (transform.childCount == 0 && fightEnded == false)
        {
            fightEnded = true;
            isFighting = false;
            crowdController.OnEndFight();
        }
    }

    private void SendChickenToMiddleOfCrowd()
    {
        float middleXValue = crowdController.GetMiddleXOfCrowd();
        float farestEnemyZValue = crowdController.GetNearestEnemyZValue();

        for (int i = 0; i < transform.childCount; i++)
        {
            var navMeshAgent = transform.GetChild(i).GetComponent<NavMeshAgent>();
            navMeshAgent.SetDestination(new Vector3(0, 0, transform.position.z - 1));
            navMeshAgent.isStopped = false;
        }
    }

    public void SpawnChickens()
    {
        for (int i = 0; i < chickensSpawnCount; i++)
        {
            var newChicken = Instantiate(chickenPrefab, transform);
            newChicken.GetComponent<Chicken>().chickenSpot = this;
        }
    }

    private float GetNearestChicken()
    {
        float minZValue = Mathf.Infinity;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).transform.position.z < minZValue)
                minZValue = transform.GetChild(i).transform.position.z;
        }
        return minZValue;
    }

    private void CheckOnStartFight()
    {
        float distanceToNearestEnemy = Mathf.Infinity;

        for (int i = 0; i < crowdController.crowdContainer.childCount; i++)
        {
            var distance = Vector3.Distance(transform.position, crowdController.crowdContainer.GetChild(i).position);

            if (distance < distanceToNearestEnemy)
                distanceToNearestEnemy = distance;
        }

        if (distanceToNearestEnemy < distanceToStartFight && isFighting == false && !fightEnded)
        {
            SendChickenToMiddleOfCrowd();
            crowdController.nearestEnemyZValue = transform.position.z - 1;
            crowdController.OnStartFight();
            isFighting = true;
        }
    }
}