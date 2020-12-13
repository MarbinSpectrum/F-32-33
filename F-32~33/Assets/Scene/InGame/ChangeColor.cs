using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public BoxCollider2D boxCollider2D;

    private bool flag = false;

    [SerializeField]
    private List<SwapColor> swapColor = new List<SwapColor>();
    [SerializeField]
    private List<SwapColor_UI> swapColor_UI = new List<SwapColor_UI>();
    [SerializeField]
    [ColorUsage(false,false)]
    private Color changeColor;
    [SerializeField]
    private float duration;


    private List<Color> swapColor_Lerp = new List<Color>();
    private List<Color> swapColor_UI_Lerp = new List<Color>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (!flag)
            {
                flag = true;
                swapColor_Lerp.Clear();
                for (int i = 0; i < swapColor.Count; i++)
                    swapColor_Lerp.Add(swapColor[i].change_Color);
                swapColor_UI_Lerp.Clear();
                for (int i = 0; i < swapColor_UI.Count; i++)
                    swapColor_UI_Lerp.Add(swapColor_UI[i].change_Color);
                StartCoroutine(NextColor());
            }
        }
    }

    IEnumerator NextColor()
    {
        duration = Mathf.Max(duration, 0.01f);
        float lerpTime = 0;
        while(lerpTime <= 1)
        {
            for (int i = 0; i < swapColor.Count; i++)
                swapColor[i].change_Color = Color.Lerp(swapColor_Lerp[i], changeColor, lerpTime);
            for (int i = 0; i < swapColor_UI.Count; i++)
                swapColor_UI[i].change_Color = Color.Lerp(swapColor_UI_Lerp[i], changeColor, lerpTime);
            lerpTime += Time.deltaTime / duration;
            yield return new WaitForFixedUpdate();
        }

    }
}
