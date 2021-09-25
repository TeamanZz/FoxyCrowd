using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class CrowdController : MonoBehaviour
{
    public Transform crowdContainer;

    [HideInInspector] public float nearestEnemyZValue;

    [SerializeField] private int startFoxesCount;
    [SerializeField] private float reducedCrowdSpeed = 0.5f;
    [SerializeField] private float defaultCrowdSpeed = 1.5f;
    [SerializeField] private GameObject foxPrefab;
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private Transform boundsControllerTransform;
    [SerializeField] private TextMeshProUGUI howMuchFoxesAdded;

    private MouseFollowing mouseFollowing;
    private float defaultFoxesObstacleAvoidanceRadius;

    private void Awake()
    {
        mouseFollowing = GameObject.FindObjectOfType<MouseFollowing>();
    }

    private void Start()
    {
        SpawnStartFoxes();
        defaultFoxesObstacleAvoidanceRadius = crowdContainer.GetChild(0).GetComponent<NavMeshAgent>().radius;
    }

    public void StopAllFoxes()
    {
        for (int i = 0; i < crowdContainer.childCount; i++)
        {
            crowdContainer.GetChild(i).GetComponent<NavMeshAgent>().isStopped = true;
        }
    }

    public void MakeStartFoxesRunning()
    {
        for (int i = 0; i < crowdContainer.childCount; i++)
        {
            crowdContainer.GetChild(i).GetComponent<Animator>().Play("Fox Run");
            crowdContainer.GetChild(i).GetComponent<NavMeshAgent>().isStopped = false;
        }
    }

    public void HandleFoxesOnEnd()
    {
        for (int i = 0; i < crowdContainer.childCount; i++)
        {
            crowdContainer.GetChild(i).GetComponent<Fox>().targetTransform = null;
            crowdContainer.GetChild(i).GetComponent<Fox>().targetPosition = new Vector3(0, 0, 200);
        }
        mouseFollowing.enabled = false;
    }

    public void ChangeObstacleAvoidanceRadius()
    {
        int foxesCount = crowdContainer.childCount;

        if (foxesCount > 100)
        {
            for (int i = 0; i < crowdContainer.childCount; i++)
            {
                crowdContainer.GetChild(i).GetComponent<NavMeshAgent>().radius = 0.24f;
            }
            return;
        }
        if (foxesCount > 80)
        {
            for (int i = 0; i < crowdContainer.childCount; i++)
            {
                crowdContainer.GetChild(i).GetComponent<NavMeshAgent>().radius = 0.26f;
            }
            return;
        }
        if (foxesCount > 60)
        {
            for (int i = 0; i < crowdContainer.childCount; i++)
            {
                crowdContainer.GetChild(i).GetComponent<NavMeshAgent>().radius = 0.3f;
            }
            return;
        }
        else
        {
            for (int i = 0; i < crowdContainer.childCount; i++)
            {
                crowdContainer.GetChild(i).GetComponent<NavMeshAgent>().radius = defaultFoxesObstacleAvoidanceRadius;
            }
            return;
        }
    }

    private void SendCrowdToMiddleOfEnemySpot()
    {
        for (int i = 0; i < crowdContainer.childCount; i++)
        {
            crowdContainer.GetChild(i).GetComponent<Fox>().targetTransform = null;
            crowdContainer.GetChild(i).GetComponent<Fox>().targetPosition = new Vector3(0, 0, nearestEnemyZValue);
            crowdContainer.GetChild(i).GetComponent<Fox>().isFighting = true;
        }
    }

    public float GetMiddleXOfCrowd()
    {
        float minX = Mathf.Infinity;
        float maxX = Mathf.NegativeInfinity;

        for (int i = 0; i < crowdContainer.childCount; i++)
        {
            float foxXPosition = crowdContainer.GetChild(i).position.x;
            if (crowdContainer.GetChild(i).position.x < minX)
            {
                minX = foxXPosition;
            }

            if (crowdContainer.GetChild(i).position.x > maxX)
            {
                maxX = foxXPosition;
            }

        }
        return ((minX + maxX) / 2);
    }

    public float GetNearestEnemyZValue()
    {
        float nearestZValue = Mathf.NegativeInfinity;

        for (int i = 0; i < crowdContainer.childCount; i++)
        {
            float foxZPosition = crowdContainer.GetChild(i).position.z;
            if (crowdContainer.GetChild(i).position.z > nearestZValue)
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
        for (int i = 0; i < crowdContainer.childCount; i++)
        {
            crowdContainer.GetChild(i).GetComponent<NavMeshAgent>().speed = defaultCrowdSpeed;
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

        for (int i = 0; i < crowdContainer.childCount; i++)
        {
            crowdContainer.GetChild(i).GetComponent<Fox>().targetTransform = crowdContainer.GetChild(i).transform.GetChild(2);
            crowdContainer.GetChild(i).GetComponent<Fox>().isFighting = false;
            crowdContainer.GetChild(i).GetComponent<Fox>().OutFromFight();
        }

        ResetCrowdContainerPosition();
    }

    private void ResetCrowdContainerPosition()
    {
        List<GameObject> tempList = new List<GameObject>();

        //Добавляем во временный список, чтобы правильно сработал SetParent
        for (int i = 0; i < crowdContainer.childCount; i++)
        {
            tempList.Add(crowdContainer.GetChild(i).gameObject);
        }

        for (int i = 0; i < tempList.Count; i++)
        {
            tempList[i].transform.SetParent(transform);
        }
        crowdContainer.transform.position = new Vector3(0, crowdContainer.transform.position.y, crowdContainer.transform.position.z);
        for (int i = 0; i < tempList.Count; i++)
        {
            tempList[i].transform.SetParent(crowdContainer.transform);
        }

        boundsControllerTransform.position = new Vector3(0, boundsControllerTransform.position.y, boundsControllerTransform.position.z);
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
            newFox.GetComponent<Animator>().Play("Fox Idle");
            newFox.GetComponent<NavMeshAgent>().isStopped = true;
        }
    }

    public void SpawnFoxes(int count, int xValue = 1)
    {
        if (xValue != 1)
            count = crowdContainer.childCount * xValue - crowdContainer.childCount;

        howMuchFoxesAdded.text = "+ " + count.ToString();
        howMuchFoxesAdded.gameObject.SetActive(false);
        howMuchFoxesAdded.gameObject.SetActive(true);

        for (int i = 0; i < count; i++)
        {
            var newFox = Instantiate(foxPrefab, crowdContainer);
            var newPos = new Vector3(crowdContainer.GetChild(0).transform.position.x, 0, crowdContainer.GetChild(0).transform.position.z);
            newFox.transform.position = newPos;
            var newTarget = Instantiate(targetPrefab, newFox.transform);
            newTarget.transform.position = new Vector3(newFox.transform.position.x, 0, newFox.transform.position.x + 10);
            newTarget.GetComponent<TargetController>().fox = newFox.transform;
            newFox.GetComponent<Fox>().targetTransform = newTarget.transform;
        }
    }

    public float GetLeftmostElement()
    {
        Transform leftmostElement = default;
        if (crowdContainer.childCount != 0)
            leftmostElement = crowdContainer.GetChild(0);

        for (int i = 0; i < crowdContainer.childCount; i++)
        {
            if (crowdContainer.GetChild(i).position.x < leftmostElement.position.x)
            {
                leftmostElement = crowdContainer.GetChild(i);
            }
        }
        return leftmostElement.position.x;
    }

    public float GetRightmostElement()
    {
        Transform rightmostElement = default;
        if (crowdContainer.childCount != 0)
            rightmostElement = crowdContainer.GetChild(0);

        for (int i = 0; i < crowdContainer.childCount; i++)
        {
            if (crowdContainer.GetChild(i).position.x > rightmostElement.position.x)
            {
                rightmostElement = crowdContainer.GetChild(i);
            }
        }
        return rightmostElement.position.x;
    }
}