using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGame : MonoBehaviour
{
    public static InGame instance;

    public SoundModule_Base soundModule;
    public Transform startPos;
    public TextMeshProUGUI scoreText;

    private float playTime = 0;
    public static int playNum = 0;

    [HideInInspector]
    public bool gameClear = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        soundModule.Play("StageInGame");
        SceneMove.instance.CaptureScreenColor(new Color(1, 1, 1), 0.1f);
    }

    private void Update()
    {
        if(!gameClear)
            playTime += Time.deltaTime;
        int temp = (int)((float)playTime * 100);
        int a = temp % 100;
        temp /= 100;
        int b = temp % 60;
        temp = temp/60;
        int c = temp;

        scoreText.text = "Time : " + c.ToString("D2") + ":" + b.ToString("D2") + ":" + a.ToString("D2") + 
            "\nDeath : " + (playNum) + "";

    }

}
