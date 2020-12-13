using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectiveBackground : MonoBehaviour
{
    public Transform targetTransform;
    public float z_value = 0f;

    private Vector3 InitialPosition;

    private void Start()
    {
        InitialPosition = transform.position;
    }

    private void Update()
    {
        transform.localPosition = new Vector3(InitialPosition.x, InitialPosition.y + targetTransform.position.y * (-z_value),0);
    }
}
