using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChickenSpot : MonoBehaviour
{
    public GameObject chickenPrefab;

    [HideInInspector] public bool isFighting = false;

    [SerializeField] private float distanceToStartFight = 2;

    private CrowdController crowdController;
    private float distanceToNearestEnemy = 0;

    private void Awake()
    {
        crowdController = GameObject.FindObjectOfType<CrowdController>();
    }

    private void Start()
    {
        SpawnChickens();
        InvokeRepeating("CheckOnStartFight", 0, 0.5f);
        InvokeRepeating("SetTargetToChickenInFight", 0, 1f);
    }

    private void SetTargetToChickenInFight()
    {
        if (!isFighting)
            return;

        for (int i = 0; i < transform.childCount; i++)
        {
            var navMeshAgent = transform.GetChild(i).GetComponent<NavMeshAgent>();
            var newEnemyIndex = Random.Range(0, crowdController.crowdTransforms.Count);
            var newEnemy = crowdController.crowdTransforms[newEnemyIndex].transform;
            if (navMeshAgent.isStopped || newEnemy.gameObject == null)
            {
                navMeshAgent.SetDestination(newEnemy.position);
                navMeshAgent.isStopped = false;
            }
        }
    }

    public void SpawnChickens()
    {
        int chickensCount = Random.Range(10, 30);
        for (int i = 0; i < chickensCount; i++)
        {
            var newChicken = Instantiate(chickenPrefab, transform);
            newChicken.GetComponent<Chicken>().chickenSpot = this;

            if (i == chickensCount - 1)
                newChicken.GetComponent<Chicken>().lastEnemy = true;
        }
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

        if (distanceToNearestEnemy < distanceToStartFight && isFighting == false)
        {
            crowdController.ReduceCrowdSpeed();
            isFighting = true;
        }
    }
}