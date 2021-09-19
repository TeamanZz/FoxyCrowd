using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EndOfLevel : MonoBehaviour
{
    private Fox fox;
    private CrowdController crowdController;
    private bool levelWasCompleted;

    private void Awake()
    {
        crowdController = GameObject.FindObjectOfType<CrowdController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (levelWasCompleted)
            return;

        if (other.TryGetComponent<Fox>(out fox))
        {
            levelWasCompleted = true;
            crowdController.HandleFoxesOnEnd();
            var oldPoints = PlayerPrefs.GetInt("PopulationPoints");
            var newPoints = oldPoints + crowdController.crowdTransforms.Count;
            PlayerPrefs.SetInt("PopulationPoints", newPoints);
            StartCoroutine(EnableSuccessScreen());
        }
    }

    private IEnumerator EnableSuccessScreen()
    {
        yield return new WaitForSeconds(4.5f);
        crowdController.EnableSuccessScreen();
    }
}