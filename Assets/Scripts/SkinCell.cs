using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinCell : MonoBehaviour
{
    [SerializeField] private GameObject skinPreview;
    [SerializeField] private GameObject questionMark;
    [SerializeField] private GameObject choosedFrame;
    public bool isOpened = false;
    public int skinIndex;

    private Button buttonComponent;

    private void Awake()
    {
        buttonComponent = GetComponent<Button>();

        if (skinIndex == 0)
        {
            PlayerPrefs.SetString($"Skin|{skinIndex}|", "Opened");
        }

        if (PlayerPrefs.GetString($"Skin|{skinIndex}|") == "Opened")
        {
            isOpened = true;
            MakeButtonClickable();
        }
    }

    public void EnableChoosedFrame()
    {
        choosedFrame.SetActive(true);
    }

    public void DisableChoosedFrame()
    {
        choosedFrame.SetActive(false);
    }

    public void MakeButtonClickable()
    {
        buttonComponent.interactable = true;
        skinPreview.SetActive(true);
        questionMark.SetActive(false);
    }
}