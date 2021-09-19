using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinsSettings : MonoBehaviour
{
    [SerializeField] private GameObject foxPrefab;
    public List<Material> foxesSkins = new List<Material>();
    [SerializeField] private Transform crowdContainer;

    private void OnEnable()
    {
        ChangeSkin();
    }

    public void ChangeSkin()
    {
        int skinIndex = PlayerPrefs.GetInt("FoxSkin");
        foxPrefab.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = foxesSkins[skinIndex];
        for (int i = 0; i < crowdContainer.childCount; i++)
        {
            crowdContainer.GetChild(i).GetChild(0).GetComponent<SkinnedMeshRenderer>().material = foxesSkins[skinIndex];
        }
    }
}