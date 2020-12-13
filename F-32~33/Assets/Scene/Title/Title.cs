using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Title : MonoBehaviour
{
    [SerializeField]
    private Button startBtn;
    [SerializeField]
    private SpriteRenderer startSprite;
    [SerializeField]
    private Button quitBtn;
    [SerializeField]
    private SpriteRenderer quitSprite;

    [SerializeField]
    private GameObject[] startSpriteList;
    [SerializeField]
    private GameObject[] quitSpriteList;

    private GameObject selectUI;

    [SerializeField]
    private Transform startBtnPos;
    [SerializeField]
    private Transform quitBtnPos;

    [SerializeField]
    private SoundModule_Base soundModule;

    private void Start()
    {
        soundModule.Play("MainMenu");
    }

    private void Update()
    {
        if (selectUI != null && EventSystem.current.currentSelectedGameObject == null)
        {
            if (selectUI.Equals(startBtn.gameObject))
                startBtn.Select();
            else if (selectUI.Equals(quitBtn.gameObject))
                quitBtn.Select();
        }
        else
            selectUI = EventSystem.current.currentSelectedGameObject;

        if(selectUI != null)
        {
            if (selectUI.Equals(startBtn.gameObject))
            {
                startSpriteList[0].SetActive(false);
                startSpriteList[1].SetActive(true);
                quitSpriteList[0].SetActive(true);
                quitSpriteList[1].SetActive(false);
            }
            else if (selectUI.Equals(quitBtn.gameObject))
            {
                startSpriteList[0].SetActive(true);
                startSpriteList[1].SetActive(false);
                quitSpriteList[0].SetActive(false);
                quitSpriteList[1].SetActive(true);
            }
        }
    }
    public void StartGame()
    {
        if(SceneMove.instance.SceneCanMove())
            SceneMove.instance.ChangeMaskPos(startBtnPos.position);
        SceneMove.instance.SceneChange(SceneName.InGame);
    }

    public void QuitGame()
    {
        if (SceneMove.instance.SceneCanMove())
            SceneMove.instance.ChangeMaskPos(quitBtnPos.position);
        SceneMove.instance.SceneChange(SceneName.GameQuit);
    }

}
