using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreensHandler : MonoBehaviour
{
    [SerializeField] private GameObject skinsScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject successScreen;

    public void EnableLoseScreen()
    {
        loseScreen.SetActive(true);
    }

    public void EnableSuccessScreen()
    {
        successScreen.SetActive(true);
    }
}