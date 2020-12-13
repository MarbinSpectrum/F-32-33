using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
[System.Serializable]
public class CutscenePair
{
    public Sprite image;
    [TextArea]
    public string text;
}

public class IntroCutscene : MonoBehaviour
{
    public TextMeshProUGUI cutsceneText;
    public Image cutsceneImage;
    public GameObject nextSceneUI;

    public CutscenePair[] cutsceneList;

    public float typewriteSpeed = 0.05f;

    public SoundModule_Base soundModule_Base;

    private int cutsceneCount = 0;
    string nowText;

    private void Start()
    {
        soundModule_Base.Play("Intro");
        OnNextCutscene();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            OnNextCutscene();
    }

    public void OnNextCutscene()
    {
        if(!nextSceneUI.activeSelf)
        {
            StopCoroutine("Cor_CutsceneTypewrite");
            nextSceneUI.SetActive(true);
            cutsceneText.text = nowText;
            return;
        }

        if (cutsceneList.Length <= cutsceneCount)
        {
            DOTween.Play("Transition_IN");
            return;
        }

        cutsceneImage.sprite = cutsceneList[cutsceneCount].image;
        nextSceneUI.SetActive(false);
        
        cutsceneText.text = String.Empty;
        StopAllCoroutines();
        StartCoroutine("Cor_CutsceneTypewrite");

        cutsceneCount++;
    }

    IEnumerator Cor_CutsceneTypewrite()
    {
        var t = cutsceneList[cutsceneCount].text;
        nowText = t;

        for (int i = 0; i <= t.Length; i++)
        {
            cutsceneText.text = t.Substring(0, i);

            yield return new WaitForSeconds(typewriteSpeed);
        }

        nextSceneUI.SetActive(true);
    }
    
}
