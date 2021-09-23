using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class SkinsScreen : MonoBehaviour
{
    public List<SkinnedMeshRenderer> foxesRenderers = new List<SkinnedMeshRenderer>();

    [SerializeField] private Transform buttonsContainer;
    [SerializeField] private TextMeshProUGUI populationPoints;
    [SerializeField] private TextMeshProUGUI newSkinCostText;

    private int newSkinCost = 250;
    private SkinsSettings skinsSettings;

    private void Awake()
    {
        SetNewSkinCost();

        skinsSettings = GameObject.FindObjectOfType<SkinsSettings>();
    }

    private void SetNewSkinCost()
    {
        var prefsNewSkinCostValue = PlayerPrefs.GetInt("NewSkinCost", 250);
        newSkinCost = prefsNewSkinCostValue;
        newSkinCostText.text = "открыть " + prefsNewSkinCostValue;
    }

    private void OnEnable()
    {
        SetPopulationPointsView();

        int prevButtonIndex = PlayerPrefs.GetInt("FoxSkin");
        EnableLastChoosedFrame(prevButtonIndex);
        SetFoxesMaterials(prevButtonIndex);
    }

    private void EnableLastChoosedFrame(int prevButtonIndex)
    {
        buttonsContainer.GetChild(prevButtonIndex).GetComponent<SkinCell>().EnableChoosedFrame();
    }

    private void SetFoxesMaterials(int index)
    {
        for (int i = 0; i < foxesRenderers.Count; i++)
        {
            foxesRenderers[i].material = skinsSettings.foxesSkins[index];
        }
    }

    private void SetPopulationPointsView()
    {
        populationPoints.text = PlayerPrefs.GetInt("PopulationPoints").ToString();
    }

    public void ChangeSkin()
    {
        var skinCell = EventSystem.current.currentSelectedGameObject.GetComponent<SkinCell>();
        var skinIndex = skinCell.skinIndex;
        int prevSkinIndex = PlayerPrefs.GetInt("FoxSkin");

        SetFoxesMaterials(skinIndex);

        buttonsContainer.GetChild(prevSkinIndex).GetComponent<SkinCell>().DisableChoosedFrame();
        buttonsContainer.GetChild(skinIndex).GetComponent<SkinCell>().EnableChoosedFrame();

        PlayerPrefs.SetInt("FoxSkin", skinIndex);
        skinsSettings.ChangeSkin();
    }

    public void BuyRandomSkin()
    {
        int currentPPCount = PlayerPrefs.GetInt("PopulationPoints");
        if (currentPPCount < newSkinCost)
            return;

        currentPPCount -= newSkinCost;
        PlayerPrefs.SetInt("NewSkinCost", newSkinCost * 2);
        SetNewSkinCost();
        PlayerPrefs.SetInt("PopulationPoints", currentPPCount);
        populationPoints.text = currentPPCount.ToString();

        List<SkinCell> notOpenedSkins = GetNotOpenedSkins();

        int newSkinIndex = Random.Range(0, notOpenedSkins.Count);
        notOpenedSkins[newSkinIndex].MakeButtonClickable();

        int skinIndex = notOpenedSkins[newSkinIndex].skinIndex;
        PlayerPrefs.SetString($"Skin|{skinIndex}|", "Opened");
    }

    private List<SkinCell> GetNotOpenedSkins()
    {
        SkinCell currentSkin;
        List<SkinCell> notOpenedSkins = new List<SkinCell>();
        for (int i = 0; i < buttonsContainer.childCount; i++)
        {
            currentSkin = buttonsContainer.GetChild(i).GetComponent<SkinCell>();
            if (!currentSkin.isOpened)
            {
                notOpenedSkins.Add(currentSkin);
            }
        }

        return notOpenedSkins;
    }
}