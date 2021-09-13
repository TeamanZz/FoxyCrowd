using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrowdController : MonoBehaviour
{
    public List<Transform> crowdTransforms = new List<Transform>();
    public Transform crowdContainer;
    public Transform foxPrefab;
    public GameObject targetPrefab;
    public int startFoxesCount;
    public int crowdSpeedPercentsReduce;

    [SerializeField] private Transform follower;
    private float targetSpawnX = 0;

    private void Start()
    {
        SpawnStartFoxes();
    }

    public void ReduceCrowdSpeed()
    {
        for (int i = 0; i < crowdTransforms.Count; i++)
        {
            crowdTransforms[i].GetComponent<NavMeshAgent>().speed -= (crowdTransforms[i].GetComponent<NavMeshAgent>().speed / 100 * crowdSpeedPercentsReduce);
        }
    }

    public void ResetCrowdSpeed()
    {
        for (int i = 0; i < crowdTransforms.Count; i++)
        {
            crowdTransforms[i].GetComponent<NavMeshAgent>().speed = 1.5f;
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
            newTarget.transform.position = new Vector3(newFox.position.x, 0, 10);
            newTarget.GetComponent<TargetController>().fox = newFox;
            newFox.GetComponent<Fox>().target = newTarget;
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
            newTarget.transform.position = new Vector3(newFox.position.x, 0, 10);
            newTarget.GetComponent<TargetController>().fox = newFox;
            newFox.GetComponent<Fox>().target = newTarget;
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