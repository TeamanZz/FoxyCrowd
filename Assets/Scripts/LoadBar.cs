using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadBar : MonoBehaviour
{
    [SerializeField] private InteractableItemsSpawner interactableItemsSpawner;
    [SerializeField] private Image filledBar;
    [SerializeField] private float fullDistance = 0;
    [SerializeField] private Transform foxFollower;
    [SerializeField] private float foxFollowerStartZ;
    [SerializeField] private TextMeshProUGUI levelText;

    private void Awake()
    {
        foxFollowerStartZ = foxFollower.position.z;
        levelText.text = PlayerPrefs.GetInt("Level", 1).ToString();
    }

    private void Update()
    {
        if (fullDistance == 0)
            fullDistance = interactableItemsSpawner.GetWayDistance(foxFollowerStartZ);

        float coveredDistance = Mathf.Abs(foxFollower.position.z - foxFollowerStartZ);
        float coveredDistanceInPercents = coveredDistance * 100 / fullDistance;
        filledBar.fillAmount = coveredDistanceInPercents / 100;
    }
}