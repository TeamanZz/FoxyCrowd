using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrowdController : MonoBehaviour
{
    [HideInInspector] public List<Transform> crowdTransforms = new List<Transform>();
    [HideInInspector] public float nearestEnemyZValue;

    [SerializeField] private GameObject foxPrefab;
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private Transform rotateTargetTransform;
    [SerializeField] private Transform crowdContainer;
    [SerializeField] private Transform follower;
    [SerializeField] private float reducedCrowdSpeed = 0.5f;
    [SerializeField] private float defaultCrowdSpeed = 1.5f;
    [SerializeField] private int startFoxesCount;

    private MouseFollowing mouseFollowing;

    private float defaultFoxesObstacleAvoidanceRadius;

    private void Awake()
    {
        mouseFollowing = GameObject.FindObjectOfType<MouseFollowing>();
    }

    private void Start()
    {
        SpawnStartFoxes();
        defaultFoxesObstacleAvoidanceRadius = crowdTransforms[0].GetComponent<NavMeshAgent>().radius;
    }

    public void HandleFoxesOnEnd()
    {
        for (int i = 0; i < crowdTransforms.Count; i++)
        {
            crowdTransforms[i].GetComponent<Fox>().targetTransform = null;
            crowdTransforms[i].GetComponent<Fox>().targetPosition = new Vector3(0, 0, 200);
        }
        mouseFollowing.enabled = false;
    }

    public void ChangeObstacleAvoidanceRadius()
    {
        int foxesCount = crowdTransforms.Count;

        if (foxesCount > 60)
        {
            for (int i = 0; i < crowdTransforms.Count; i++)
            {
                crowdTransforms[i].GetComponent<NavMeshAgent>().radius = 0.3f;
            }
        }
        else
        {
            for (int i = 0; i < crowdTransforms.Count; i++)
            {
                crowdTransforms[i].GetComponent<NavMeshAgent>().radius = defaultFoxesObstacleAvoidanceRadius;
            }
        }
    }

    private void SendCrowdToMiddleOfEnemySpot()
    {
        for (int i = 0; i < crowdTransforms.Count; i++)
        {
            crowdTransforms[i].GetComponent<Fox>().targetTransform = null;
            crowdTransforms[i].GetComponent<Fox>().targetPosition = new Vector3(0, 0, nearestEnemyZValue);
            crowdTransforms[i].GetComponent<Fox>().isFighting = true;
        }
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
            crowdTransforms[i].GetComponent<NavMeshAgent>().speed = defaultCrowdSpeed;
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
            crowdTransforms[i].GetComponent<Fox>().isFighting = false;
            crowdTransforms[i].GetComponent<Fox>().OutFromFight();
        }

        ResetCrowdAndInputTargets();
    }

    private void ResetCrowdAndInputTargets()
    {
        for (int i = 0; i < crowdTransforms.Count; i++)
        {
            crowdTransforms[i].transform.SetParent(transform);
        }

        crowdContainer.transform.position = new Vector3(0, crowdContainer.transform.position.y, crowdContainer.transform.position.z);

        for (int i = 0; i < crowdTransforms.Count; i++)
        {
            crowdTransforms[i].transform.SetParent(crowdContainer.transform);
        }

        rotateTargetTransform.position = new Vector3(0, rotateTargetTransform.position.y, rotateTargetTransform.position.z);
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
            var newTarget = Instantiate(targetPrefab, newFox.transform);
            newTarget.transform.position = new Vector3(newFox.transform.position.x, 0, newFox.transform.position.x + 10);
            newTarget.GetComponent<TargetController>().fox = newFox.transform;
            newFox.GetComponent<Fox>().targetTransform = newTarget.transform;
            crowdTransforms.Add(newFox.transform);
        }
    }

    public void SpawnFoxes(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var newFox = Instantiate(foxPrefab, crowdContainer);
            newFox.transform.position = follower.transform.position;
            var newTarget = Instantiate(targetPrefab, newFox.transform);
            newTarget.transform.position = new Vector3(newFox.transform.position.x, 0, newFox.transform.position.x + 10);
            newTarget.GetComponent<TargetController>().fox = newFox.transform;
            newFox.GetComponent<Fox>().targetTransform = newTarget.transform;
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