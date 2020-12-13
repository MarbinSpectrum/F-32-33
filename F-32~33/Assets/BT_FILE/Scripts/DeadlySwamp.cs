using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlySwamp : MonoBehaviour
{
    public float swampSpeed;
    public float stanbyTime = 2.0f;
    public bool startOnAwake = true;

    public Transform SwampHead;
    public Transform SwampMiddle;

    public float maxY;
    
    private void Start()
    {
        Debug.Log(LevelObjectsManager.Instance);
        LevelObjectsManager.Instance.ONPlayerHurt += StopSwamp;
        Debug.Log("d1");
        
        if (startOnAwake)
            StartCoroutine(Cor_SwampFillup());
        
    }
    

    private void OnDisable()
    {
        LevelObjectsManager.Instance.ONPlayerHurt -= StopSwamp;
    }

    public void StartSwamp()
    {
        StartCoroutine(Cor_SwampFillup());
    }

    public void StopSwamp()
    {
        StopAllCoroutines();
    }

    IEnumerator Cor_SwampFillup()
    {
        //a
        yield return new WaitForSeconds(stanbyTime);
        
        while (true)
        {
            yield return new WaitForFixedUpdate();
            SwampMiddle.localScale = new Vector2(SwampMiddle.localScale.x, SwampMiddle.localScale.y + swampSpeed);
            SwampMiddle.localScale = new Vector2(SwampMiddle.localScale.x, Mathf.Min(maxY + 5, SwampMiddle.localScale.y));
            SwampHead.position = new Vector2(SwampHead.position.x, SwampHead.position.y + swampSpeed);
            SwampHead.position = new Vector2(SwampHead.position.x, Mathf.Min(maxY - 1, SwampHead.position.y));
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector3(-100, maxY, 0), new Vector3(+100, maxY, 0));
    }


}
