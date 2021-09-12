using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fox : MonoBehaviour
{
    [HideInInspector] public GameObject target;

    private Transform lookTarget;
    private NavMeshAgent agent;

    private void Awake()
    {
        lookTarget = GameObject.Find("Rotate Target").transform;
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        agent.SetDestination(target.transform.position);
        transform.LookAt(lookTarget, Vector3.left);
    }
}