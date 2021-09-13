using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fox : MonoBehaviour
{
    public Vector3 targetPosition;
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        agent.SetDestination(targetPosition);
        transform.LookAt(new Vector3(transform.position.x, 0, transform.position.z + 1), Vector3.left);
    }
}