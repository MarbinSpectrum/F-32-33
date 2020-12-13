using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject pause;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pause.activeSelf)
                Time.timeScale = 1;
            else
                Time.timeScale = 0;
            pause.SetActive(!pause.activeSelf);
        }
    }
}
