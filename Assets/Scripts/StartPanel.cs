using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartPanel : MonoBehaviour
{
    [SerializeField] private CrowdController crowdController;
    [SerializeField] private MouseFollowing mouseFollowing;
    [SerializeField] private TextMeshProUGUI populationCountText;
    [SerializeField] private GameObject foxesCountText;

    private void Awake()
    {
        // PlayerPrefs.DeleteAll();
    }

    private void Start()
    {
        LoadPopulationPoints();
    }

    private void LoadPopulationPoints()
    {
        var points = PlayerPrefs.GetInt("PopulationPoints");
        populationCountText.text = points.ToString();
    }

    public void OnStartPanelClick()
    {
        crowdController.MakeStartFoxesRunning();
        foxesCountText.SetActive(true);
        gameObject.SetActive(false);
    }
}