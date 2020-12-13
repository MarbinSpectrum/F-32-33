using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GlowShader : MonoBehaviour
{
    private MaterialPropertyBlock mpb;

    [ColorUsage(false,true)] public Color glow_Color;
    public Texture2D glow_Mask;

    [Space(20)]
    [Header("-------------------------------------------")]

    public SpriteRenderer spriteRenderer;

    private void Update()
    {
        UpdateOutline();
    }
    private void UpdateOutline()
    {
        if (spriteRenderer == null)
            return;

        if (mpb == null)
            mpb = new MaterialPropertyBlock();

        spriteRenderer.GetPropertyBlock(mpb);
        mpb.SetColor("_GlowColor", glow_Color);
        mpb.SetTexture("_GlowMask", glow_Mask);
        spriteRenderer.SetPropertyBlock(mpb);
      
    }
}
