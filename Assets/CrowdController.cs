using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdController : MonoBehaviour
{
    public List<Transform> crowdTransforms = new List<Transform>();
    public Transform crowdContainer;
    public Transform foxPrefab;
    public GameObject targetPrefab;

    public int startFoxesCount;

    private float targetSpawnX = 0;

    private void Start()
    {
        SpawnStartFoxes();
    }

    private void SpawnStartFoxes()
    {
        for (int i = 0; i < startFoxesCount; i++)
        {
            var newFox = Instantiate(foxPrefab, crowdContainer);
            var newTarget = Instantiate(targetPrefab, new Vector3(newFox.position.x, 0, 10), Quaternion.identity);
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