using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fox : MonoBehaviour
{
    private CrowdController crowdController;
    [HideInInspector] public GameObject target;
    private NavMeshAgent agent;
    private Transform lookTarget;

    private void Awake()
    {
        crowdController = GameObject.FindObjectOfType<CrowdController>();
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