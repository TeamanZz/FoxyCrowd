﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrowdController : MonoBehaviour
{
    public List<Transform> crowdTransforms = new List<Transform>();
    public Transform crowdContainer;
    public GameObject targetPrefab;
    public Transform foxPrefab;
    public int startFoxesCount;

    [SerializeField] private float reducedCrowdSpeed = 0.5f;
    [SerializeField] private Transform follower;

    private MouseFollowing mouseFollowing;
    private float defaultCrowdSpeed = 0;
    public float nearestEnemyZValue;

    private void Awake()
    {
        mouseFollowing = GameObject.FindObjectOfType<MouseFollowing>();
    }

    private void Start()
    {
        SpawnStartFoxes();
        defaultCrowdSpeed = crowdTransforms[0].GetComponent<NavMeshAgent>().speed;
    }

    private void SendCrowdToMiddleOfEnemySpot()
    {
        for (int i = 0; i < crowdTransforms.Count; i++)
            crowdTransforms[i].GetComponent<Fox>().targetPosition = new Vector3(0, 0, nearestEnemyZValue);
    }

    public float GetMiddleXOfCrowd()
    {
        float minX = Mathf.Infinity;
        float maxX = Mathf.NegativeInfinity;

        for (int i = 0; i < crowdTransforms.Count; i++)
        {
            float foxXPosition = crowdTransforms[i].position.x;
            if (crowdTransforms[i].position.x < minX)
            {
                minX = foxXPosition;
            }

            if (crowdTransforms[i].position.x > maxX)
            {
                maxX = foxXPosition;
            }

        }
        return ((minX + maxX) / 2);
    }

    public float GetNearestEnemyZValue()
    {
        float nearestZValue = Mathf.NegativeInfinity;

        for (int i = 0; i < crowdTransforms.Count; i++)
        {
            float foxZPosition = crowdTransforms[i].position.z;
            if (crowdTransforms[i].position.z > nearestZValue)
            {
                nearestZValue = foxZPosition;
            }
        }
        return nearestZValue;

    }

    public void ReduceCrowdSpeed()
    {

        for (int i = 0; i < crowdContainer.childCount; i++)
        {
            crowdContainer.GetChild(i).GetComponent<NavMeshAgent>().speed = reducedCrowdSpeed;
        }
    }

    public void ResetCrowdSpeed()
    {
        for (int i = 0; i < crowdTransforms.Count; i++)
        {
            crowdTransforms[i].GetComponent<NavMeshAgent>().speed = 1.5f;
        }
    }

    public void OnStartFight()
    {
        SendCrowdToMiddleOfEnemySpot();
        ReduceCrowdSpeed();
        mouseFollowing.enabled = false;
    }

    public void OnEndFight()
    {
        ResetCrowdSpeed();
        mouseFollowing.enabled = true;

        for (int i = 0; i < crowdTransforms.Count; i++)
        {
            crowdTransforms[i].GetComponent<Fox>().targetTransform = crowdTransforms[i].transform.GetChild(2);
        }
    }

    public void RemoveFromCrowd(Transform fox)
    {
        crowdTransforms.Remove(fox);
    }

    private void SpawnStartFoxes()
    {
        for (int i = 0; i < startFoxesCount; i++)
        {
            var newFox = Instantiate(foxPrefab, crowdContainer);
            var newTarget = Instantiate(targetPrefab, newFox);
            newTarget.transform.position = new Vector3(newFox.position.x, 0, newFox.position.x + 10);
            newTarget.GetComponent<TargetController>().fox = newFox;
            newFox.GetComponent<Fox>().targetPosition = newTarget.transform.position;
            crowdTransforms.Add(newFox.transform);
        }
    }

    public void SpawnFoxes(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var newFox = Instantiate(foxPrefab, crowdContainer);
            newFox.position = follower.transform.position;
            var newTarget = Instantiate(targetPrefab, newFox);
            newTarget.transform.position = new Vector3(newFox.position.x, 0, newFox.position.x + 10);
            newTarget.GetComponent<TargetController>().fox = newFox;
            newFox.GetComponent<Fox>().targetPosition = newTarget.transform.position;
            crowdTransforms.Add(newFox.transform);
        }
    }

    public float GetLeftmostElement()
    {
        Transform leftmostElement = default;
        if (crowdTransforms.Count != 0)
            leftmostElement = crowdTransforms[0];

        for (int i = 0; i < crowdTransforms.Count; i++)
        {
            if (crowdTransforms[i].position.x < leftmostElement.position.x)
            {
                leftmostElement = crowdTransforms[i];
            }
        }
        return leftmostElement.position.x;
    }

    public float GetRightmostElement()
    {
        Transform rightmostElement = default;
        if (crowdTransforms.Count != 0)
            rightmostElement = crowdTransforms[0];

        for (int i = 0; i < crowdTransforms.Count; i++)
        {
            if (crowdTransforms[i].position.x > rightmostElement.position.x)
            {
                rightmostElement = crowdTransforms[i];
            }
        }
        return rightmostElement.position.x;
    }
}