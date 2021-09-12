using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chicken : MonoBehaviour
{
    private Fox fox;
    private bool wasCollided = false;
    private CrowdController crowdController;
    [HideInInspector] public ChickenSpot chickenSpot;
    public bool lastEnemy;

    private void Awake()
    {
        crowdController = GameObject.FindObjectOfType<CrowdController>();
    }

    private void Start()
    {
        StartCoroutine(SetTargetOnStart());
    }

    private IEnumerator SetTargetOnStart()
    {
        GetComponent<NavMeshAgent>().SetDestination(crowdController.crowdTransforms[0].position);
        // GetComponent<NavMeshAgent>().SetDestination(Vector3.zero);
        yield return new WaitForSeconds(0.5f);
        GetComponent<NavMeshAgent>().isStopped = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Fox>(out fox))
        {
            if (wasCollided)
                return;

            if (lastEnemy)
            {
                crowdController.ResetCrowdSpeed();
            }

            crowdController.RemoveFromCrowd(fox.transform);

            Destroy(fox.gameObject);
            Destroy(gameObject);
            wasCollided = true;
        }
    }
}