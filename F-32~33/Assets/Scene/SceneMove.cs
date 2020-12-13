using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public enum SceneName
{
    Title,GameQuit,InGame
}

public class SceneMove : MonoBehaviour
{
    public static SceneMove instance;

    [SerializeField]
    private SpriteRenderer screenRenderer;
    [SerializeField]
    private GameObject mask;

    private Camera camera;

    private float moveDelay;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    public void Update()
    {
        if (moveDelay > 0)
            moveDelay -= Time.deltaTime;
        if(camera == null)
            camera = FindObjectOfType<Camera>();
        screenRenderer.transform.position = new Vector3(screenRenderer.transform.position.x, camera.transform.position.y - 5.78f, screenRenderer.transform.position.z);
    }

    public bool SceneCanMove()
    {
        return moveDelay <= 0;
    }

    public void SceneChange(SceneName sceneName)
    {
        SceneChange(sceneName.ToString());
    }

    public void SceneChange(string sceneName)
    {
        if (SceneCanMove())
            StartCoroutine(CaptureScreen(sceneName));
    }

    public void ChangeMaskPos(Vector2 pos)
    {
        mask.transform.position = pos;
    }

    IEnumerator CaptureScreen(string sceneName)
    {
        mask.SetActive(false);
        moveDelay = 1f;
        screenRenderer.sprite = null;
        Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        yield return new WaitForEndOfFrame();
        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
        texture.Apply();
        screenRenderer.sprite = Sprite.Create(texture, new Rect(Vector2.zero, new Vector2(texture.width, texture.height)),Vector2.zero);
        mask.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(sceneName);
    }

    public void CaptureScreenColor(Color color, float duration = 1)
    {
        StartCoroutine(Cor_CaptureScreenColor(color, duration));
    }

    IEnumerator Cor_CaptureScreenColor(Color color,float duration)
    {
        duration = Mathf.Max(duration, 1f);
        float lerpTime = 0;
        Color lerpColor = screenRenderer.color;
        while (lerpTime <= 1)
        {
            screenRenderer.color = Color.Lerp(lerpColor, color, lerpTime);
            lerpTime += Time.deltaTime / duration;
            yield return new WaitForFixedUpdate();
        }
    }


}
