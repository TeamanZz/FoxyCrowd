using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fox : MonoBehaviour
{
    public Vector3 targetPosition;
    public Transform targetTransform;

    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        if (targetTransform != null)
        {
            agent.SetDestination(targetTransform.position);
        }
        else
        {
            agent.SetDestination(targetPosition);
        }
    }
}