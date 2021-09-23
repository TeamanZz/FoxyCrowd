using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private GameObject triggerParticles;

    private void OnTriggerEnter(Collider other)
    {
        Fox fox;
        if (other.TryGetComponent<Fox>(out fox))
        {
            Instantiate(triggerParticles, gameObject.transform.position, Quaternion.identity);
            Destroy(fox.gameObject);
        }
    }
}
