using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EndOfLevel : MonoBehaviour
{
    private Fox fox;
    private CrowdController crowdController;

    private void Awake()
    {
        crowdController = GameObject.FindObjectOfType<CrowdController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Fox>(out fox))
        {
            crowdController.HandleFoxesOnEnd();
        }
    }
}
