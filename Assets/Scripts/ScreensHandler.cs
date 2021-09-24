using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreensHandler : MonoBehaviour
{
    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject skinsScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject successScreen;
    [SerializeField] private MouseFollowing mouseFollowing;

    [SerializeField] private GameObject skinsScreenCamera;

    public void EnableSkinsScreen()
    {
        skinsScreen.SetActive(true);
        skinsScreenCamera.SetActive(true);
        startScreen.SetActive(false);
        mouseFollowing.enabled = false;
    }

    public void EnableStartScreen()
    {
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