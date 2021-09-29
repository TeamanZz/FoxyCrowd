using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chicken : MonoBehaviour
{
    [HideInInspector] public ChickenSpot chickenSpot;

    private CrowdController crowdController;
    private bool wasCollided = false;
    private Fox foxTarget;

    [SerializeField] private GameObject onDeathParticlesPrefab;
    [SerializeField] private GameObject onDeathFoxParticlesPrefab;

    private void Awake()
    {
        crowdController = GameObject.FindObjectOfType<CrowdController>();
    }

    private void Start()
    {
        StartCoroutine(SetTargetOnStart());
    }

    private IEnumerator SetTargetOnStart()
    {
        GetComponent<NavMeshAgent>().SetDestination(new Vector3(0, 0, -10));
        yield return new WaitForSeconds(1.5f);
        GetComponent<NavMeshAgent>().isStopped = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Fox>(out foxTarget))
        {
            if (wasCollided)
                return;
            // Handheld.Vibrate();
            Instantiate(onDeathParticlesPrefab, transform.position, Quaternion.identity);
            // Instantiate(onDeathFoxParticlesPrefab, transform.position, Quaternion.identity);
            // Destroy(foxTarget.gameObject);
            foxTarget.KillFox("Chicken");
            Destroy(gameObject);
            wasCollided = true;
        }
    }
}