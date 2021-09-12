using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxMultiply : MonoBehaviour
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
            crowdController.SpawnFoxes(5);
            Destroy(gameObject);
        }
    }
}
