using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameAnalyticsSDK;

public class GameStateHandler : MonoBehaviour
{
    [SerializeField] private Transform crowdContainer;
    private ScreensHandler screensHandler;
    private CrowdController crowdController;

    private void Awake()
    {
        screensHandler = GetComponent<ScreensHandler>();
        crowdController = GetComponent<CrowdController>();
    }

    private void Start()
    {
        InvokeRepeating("CheckOnLose", 1, 1);
    }

    private void CheckOnLose()
    {
        if (crowdContainer.childCount == 0)
            StartCoroutine(EnableLoseScreen());
    }

    public void CheckOnSuccess()
    {
        crowdController.HandleFoxesOnEnd();
        StartCoroutine(EnableSuccessScreen());
    }

    private IEnumerator EnableSuccessScreen()
    {
        yield return new WaitForSeconds(3f);
        crowdController.StopAllFoxes();
        var lastLevel = PlayerPrefs.GetInt("Level", 1);
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Level " + lastLevel);
        lastLevel++;
        PlayerPrefs.SetInt("Level", lastLevel);
        screensHandler.EnableSuccessScreen();
    }

    private IEnumerator EnableLoseScreen()
    {
        var lastLevel = PlayerPrefs.GetInt("Level", 1);
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "Level " + lastLevel);
        yield return new WaitForSeconds(2f);
        screensHandler.EnableLoseScreen();
    }

    public void RestartGame()
    {
        SFXHandler.sFXHandler.PlayTap();
        AudioHandler.audioHandler.audioSource.volume = 1;
        SceneManager.LoadScene(0);
    }
}