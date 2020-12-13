using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearSaw : MonoBehaviour
{
    public GameObject bladeObject;

    public Transform posA;
    public Transform posB;

    public float interval = 2.0f;

    private float t;

    public void Update()
    {
        t += Time.deltaTime;

        if (t % interval * 2 < interval)
            bladeObject.transform.position =
                Vector3.Lerp(posA.position, posB.position, (t % interval) / interval * 2);
        else
            bladeObject.transform.position =
                Vector3.Lerp(posA.position, posB.position, (1 - ((t % interval) / interval)) * 2);
    }
    
}
