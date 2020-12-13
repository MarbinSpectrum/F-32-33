using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuReturn : MonoBehaviour
{
    private void Update()
    {
        if(Input.anyKey || Input.GetMouseButtonDown(0))
            SceneMove.instance.SceneChange("Title");
    }
}
