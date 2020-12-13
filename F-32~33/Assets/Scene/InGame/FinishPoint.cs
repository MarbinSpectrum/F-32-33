using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


public class FinishPoint : MonoBehaviour
{
    [Required]
    public Transform VolcanoMaw;
    [Required]
    public GameObject GameClearUI;
    [Required]
    public Player player;

    public SpriteRenderer spriteRenderer;

    bool flag = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!flag && other.gameObject == player.gameObject)
        {
            flag = true;
            StartCoroutine(Cor_FinishGame());
        }
            
    }

    IEnumerator Cor_FinishGame()
    {
        yield return null;
        yield return StartCoroutine(player.Cor_ThowCapsule(player.transform.position, VolcanoMaw.position, 1f));
        spriteRenderer.sortingOrder = 1;
        InGame.instance.soundModule.Fadeout(1f);
        yield return new WaitForSeconds(1.0f);
        GameClearUI.SetActive(true);
    }
}
