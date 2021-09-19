using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinsScreen : MonoBehaviour
{
    private SkinsSettings skinsSettings;
    public List<SkinnedMeshRenderer> foxesRenderers = new List<SkinnedMeshRenderer>();
    [SerializeField] private Transform buttonsContainer;

    private void Awake()
    {
        skinsSettings = GameObject.FindObjectOfType<SkinsSettings>();
    }

    private void OnEnable()
    {
        int prevButtonIndex = PlayerPrefs.GetInt("FoxSkin");

        buttonsContainer.GetChild(prevButtonIndex).GetChild(0).gameObject.SetActive(false);
        buttonsContainer.GetChild(prevButtonIndex).GetChild(1).gameObject.SetActive(true);

        for (int i = 0; i < foxesRenderers.Count; i++)
        {
            foxesRenderers[i].material = skinsSettings.foxesSkins[prevButtonIndex];
        }
    }

    public void ChangeSkin(int index)
    {
        for (int i = 0; i < foxesRenderers.Count; i++)
        {
            foxesRenderers[i].material = skinsSettings.foxesSkins[index];
        }

        int prevButtonIndex = PlayerPrefs.GetInt("FoxSkin");

        buttonsContainer.GetChild(prevButtonIndex).GetChild(0).gameObject.SetActive(true);
        buttonsContainer.GetChild(prevButtonIndex).GetChild(1).gameObject.SetActive(false);

        buttonsContainer.GetChild(index).GetChild(0).gameObject.SetActive(false);
        buttonsContainer.GetChild(index).GetChild(1).gameObject.SetActive(true);

        PlayerPrefs.SetInt("FoxSkin", index);
        skinsSettings.ChangeSkin();
    }
}