using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinsScreen : MonoBehaviour
{
    [SerializeField] private List<Material> foxMaterials = new List<Material>();
    public List<SkinnedMeshRenderer> foxesRenderers = new List<SkinnedMeshRenderer>();

    public void ChangeSkin(int index)
    {
        for (int i = 0; i < foxesRenderers.Count; i++)
        {
            foxesRenderers[i].material = foxMaterials[index];
        }
    }
}