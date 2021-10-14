using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using GameAnalyticsSDK;

public class EndOfLevel : MonoBehaviour
{
    private bool levelWasCompleted;

    private CrowdController crowdController;
    private ScreensHandler screensHandler;
    private GameStateHandler gameStateHandler;

    [SerializeField] private List<GameObject> particles = new List<GameObject>();

    private void Awake()
    {
        crowdController = GameObject.FindObjectOfType<CrowdController>();
        screensHandler = GameObject.FindObjectOfType<ScreensHandler>();
        gameStateHandler = GameObject.FindObjectOfType<GameStateHandler>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (levelWasCompleted)
            return;

        Fox finishedFox;
        if (other.TryGetComponent<Fox>(out finishedFox))
        {
            foreach (var particle in particles)
            {
                particle.SetActive(true);
            }
            AudioHandler.audioHandler.audioSource.volume = 0f;
            SFXHandler.sFXHandler.PlaySuccess();
            levelWasCompleted = true;
            IncreasePopulationPoints();
            gameStateHandler.CheckOnSuccess();
        }
    }

    private void IncreasePopulationPoints()
    {
        var savedFoxesCount = crowdController.crowdContainer.childCount;
        GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "PopulationPoints", savedFoxesCount, "Gameplay", "EndOfLevel");
        var oldPoints = PlayerPrefs.GetInt("PopulationPoints");
        var newPoints = oldPoints + savedFoxesCount;
        PlayerPrefs.SetInt("PopulationPoints", newPoints);
    }
}