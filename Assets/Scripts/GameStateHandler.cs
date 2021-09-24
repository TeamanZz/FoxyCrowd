using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        yield return new WaitForSeconds(4.5f);
        crowdController.StopAllFoxes();
        var lastLevel = PlayerPrefs.GetInt("Level", 1);
        lastLevel++;
        PlayerPrefs.SetInt("Level", lastLevel);
        screensHandler.EnableSuccessScreen();
    }

    private IEnumerator EnableLoseScreen()
    {
        yield return new WaitForSeconds(2f);
        screensHandler.EnableLoseScreen();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}