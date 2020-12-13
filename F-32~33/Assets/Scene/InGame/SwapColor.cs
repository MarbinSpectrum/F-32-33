using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SwapColor : MonoBehaviour
{
    private MaterialPropertyBlock mpb;

    [ColorUsage(false, false)] public Color base_Color;
    [ColorUsage(false, false)] public Color change_Color;

    [Space(20)]
    [Header("-------------------------------------------")]

    public Renderer renderer;

    private void Update()
    {
        UpdateOutline();
    }
    private void UpdateOutline()
    {
        if (renderer == null)
            return;

        if (mpb == null)
            mpb = new MaterialPropertyBlock();

        renderer.GetPropertyBlock(mpb);
        mpb.SetColor("_BaseColor", base_Color);
        mpb.SetColor("_ChangeColor", change_Color);
        renderer.SetPropertyBlock(mpb);
      
    }
}
