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
    [SerializeField] private GameObject progressBar;
    [SerializeField] private GameObject settingsPanel;

    private void Start()
    {
        LoadPopulationPoints();
    }

    public void ToggleSettingsPanel()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
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
        progressBar.SetActive(true);
        SFXHandler.sFXHandler.PlayStartMoving();
        gameObject.SetActive(false);
    }
}