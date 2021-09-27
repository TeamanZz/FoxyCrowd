using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreensHandler : MonoBehaviour
{
    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject skinsObjects;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject skinsScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject successScreen;
    [SerializeField] private MouseFollowing mouseFollowing;

    [SerializeField] private GameObject skinsScreenCamera;

    public void EnableSkinsScreen()
    {
        skinsObjects.SetActive(true);
        skinsScreen.SetActive(true);
        skinsScreenCamera.SetActive(true);
        startScreen.SetActive(false);
        mainCamera.SetActive(false);
        mouseFollowing.enabled = false;
    }

    public void EnableStartScreen()
    {
        mainCamera.SetActive(true);
        skinsObjects.SetActive(false);
        skinsScreenCamera.SetActive(false);
        startScreen.SetActive(true);
        skinsScreen.SetActive(false);
        mouseFollowing.enabled = true;
    }

    public void EnableLoseScreen()
    {
        loseScreen.SetActive(true);
    }

    public void EnableSuccessScreen()
    {
        successScreen.SetActive(true);
    }
}