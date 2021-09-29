using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private GameObject triggerParticles;
    [SerializeField] private TrapType trapType;
    [SerializeField] private Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        Fox fox;
        if (other.TryGetComponent<Fox>(out fox))
        {
            if (trapType == TrapType.Grass)
                animator.Play("Grass Animation");
            if (trapType == TrapType.Bush)
                animator.Play("Bush Animation");
            Instantiate(triggerParticles, transform.position, Quaternion.identity);
            fox.KillFox("Trap");
        }
    }

    public enum TrapType
    {
        Grass,
        Bush
    }
}
