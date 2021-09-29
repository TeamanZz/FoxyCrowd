using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fox : MonoBehaviour
{
    public Vector3 targetPosition;
    public Transform targetTransform;

    private NavMeshAgent agent;
    private Animator animator;
    private CrowdController crowdController;

    public bool isFighting = false;

    [SerializeField] private float distanceToStartFightAnimation;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private SFXHandler SFXHandler;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        crowdController = GameObject.FindObjectOfType<CrowdController>();
        SFXHandler = GameObject.FindObjectOfType<SFXHandler>();
        audioSource = SFXHandler.GetComponent<AudioSource>();
    }

    public void KillFox(string deathSource)
    {
        if (transform.parent.childCount == 1)
            SFXHandler.sFXHandler.PlayLose();

        int foxDeathSoundIndex = 0;
        if (deathSource == "Trap")
        {
            foxDeathSoundIndex = Random.Range(0, SFXHandler.foxDeathTrap.Count);
            audioSource.PlayOneShot(SFXHandler.foxDeathTrap[foxDeathSoundIndex]);

        }
        else if (deathSource == "Chicken")
        {
            foxDeathSoundIndex = Random.Range(0, SFXHandler.foxDeathChicken.Count);
            audioSource.PlayOneShot(SFXHandler.foxDeathChicken[foxDeathSoundIndex]);
        }

        Destroy(gameObject);
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("CheckIsEnemyNearExist", 1, 1);
    }

    public void OutFromFight()
    {
        animator.Play("Fox Run");
    }

    private void CheckIsEnemyNearExist()
    {
        if (!isFighting)
            return;

        var distanceToFightPoint = crowdController.nearestEnemyZValue - transform.position.z;
        if (distanceToFightPoint <= distanceToStartFightAnimation)
        {
            PlayFightAnimation();
        }
    }

    private void PlayFightAnimation()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Fox Attack"))
            animator.Play("Fox Attack");
    }

    private void FixedUpdate()
    {
        SetDestination();
    }

    private void SetDestination()
    {
        if (targetTransform != null)
            agent.SetDestination(targetTransform.position);
        else
            agent.SetDestination(targetPosition);
    }
}