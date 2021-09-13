using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chicken : MonoBehaviour
{
    public Fox foxTarget;
    private bool wasCollided = false;
    private CrowdController crowdController;
    [HideInInspector] public ChickenSpot chickenSpot;

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
        yield return new WaitForSeconds(1f);
        GetComponent<NavMeshAgent>().isStopped = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Fox>(out foxTarget))
        {
            if (wasCollided)
                return;

            crowdController.RemoveFromCrowd(foxTarget.transform);

            Destroy(foxTarget.gameObject);
            Destroy(gameObject);
            wasCollided = true;
        }
    }
}