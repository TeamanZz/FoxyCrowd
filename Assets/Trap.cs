using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private GameObject triggerParticles;
    [SerializeField] private TrapType trapType;

    private void OnTriggerEnter(Collider other)
    {
        Fox fox;
        if (other.TryGetComponent<Fox>(out fox))
        {
            if (trapType == TrapType.Grass)
                GetComponent<Animator>().Play("Grass Animation");
            if (trapType == TrapType.Bush)
                GetComponent<Animator>().Play("Bush Animation");
            // Handheld.Vibrate();
            Instantiate(triggerParticles, gameObject.transform.position, Quaternion.identity);
            Destroy(fox.gameObject);
        }
    }

    public enum TrapType
    {
        Grass,
        Bush
    }
}
