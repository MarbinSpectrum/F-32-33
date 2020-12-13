using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class LevelObjectsManager : MonoBehaviour
{
    public static LevelObjectsManager Instance = null;

     
    public delegate void OnPlayerHurt();  

    public OnPlayerHurt ONPlayerHurt; // 여기에 플레이어가 피해를 입었을때 호출되는 함수들을 추가해주세요

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        {
            Destroy(this.gameObject);
        }
        Instance = this;
    }

}
