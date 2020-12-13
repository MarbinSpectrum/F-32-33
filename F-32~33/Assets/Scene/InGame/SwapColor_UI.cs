using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class SwapColor_UI : MonoBehaviour
{
    private Material mat;

    [ColorUsage(false, false)] public Color base_Color;
    [ColorUsage(false, false)] public Color change_Color;

    [Space(20)]
    [Header("-------------------------------------------")]

    public Image renderer;

    private void Update()
    {
        UpdateOutline();
    }
    private void UpdateOutline()
    {
        if (renderer == null)
            return;

        if (mat == null)
            mat = Instantiate(renderer.material);

        mat.SetColor("_BaseColor", base_Color);
        mat.SetColor("_ChangeColor", change_Color);
        renderer.material = mat;

    }
}
