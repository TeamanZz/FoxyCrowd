using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChickenSpot : MonoBehaviour
{
    public GameObject chickenPrefab;

    [HideInInspector] public bool isFighting = false;
    [HideInInspector] public bool fightEnded = false;

    [SerializeField] private float distanceToStartFight = 2;

    private CrowdController crowdController;
    private float distanceToNearestEnemy = 0;

    private void Awake()
    {
    }

    private void Start()
    {
        crowdController = GameObject.FindObjectOfType<CrowdController>();
        SpawnChickens();
        InvokeRepeating("CheckOnStartFight", 0, 0.5f);
        InvokeRepeating("CheckOnEndFight", 0, 0.5f);
    }

    private void CheckOnEndFight()
    {
        if (transform.childCount == 0)
        {
            fightEnded = true;
            isFighting = false;
            crowdController.OnEndFight();
        }
    }

    private void SendChickenToMiddleOfCrowd()
    {
        float middleXValue = crowdController.GetMiddleXOfCrowd();
        float farestEnemyZValue = crowdController.GetFarestEnemyZValue();

        for (int i = 0; i < transform.childCount; i++)
        {
            var navMeshAgent = transform.GetChild(i).GetComponent<NavMeshAgent>();
            navMeshAgent.SetDestination(new Vector3(0, 0, farestEnemyZValue));
            navMeshAgent.isStopped = false;
        }
    }

    public void SpawnChickens()
    {
        int chickensCount = Random.Range(10, 30);
        for (int i = 0; i < chickensCount; i++)
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

        for (int i = 0; i < crowdController.crowdTransforms.Count; i++)
        {
            var distance = Vector3.Distance(transform.position, crowdController.crowdTransforms[i].position);

            if (distance < distanceToNearestEnemy)
                distanceToNearestEnemy = distance;
        }

        if (distanceToNearestEnemy < distanceToStartFight && isFighting == false && !fightEnded)
        {
            Debug.Log("Fight Time");
            SendChickenToMiddleOfCrowd();
            crowdController.nearestEnemyZValue = GetNearestChicken();
            crowdController.OnStartFight();
            isFighting = true;
        }
    }
}