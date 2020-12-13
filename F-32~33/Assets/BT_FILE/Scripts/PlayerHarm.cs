using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerHarm : MonoBehaviour
{
    private LevelObjectsManager loManager;

    private void Start()
    {
        loManager = LevelObjectsManager.Instance;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) // 버그 가능성 있음
        {
            if(!InGame.instance.gameClear)
                loManager.ONPlayerHurt();
        }
    }
}
