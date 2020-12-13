using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    private Animator animator;
    private Rigidbody2D rigidbody2D;
    private BoxCollider2D boxCollider2D;
    private SpriteRenderer spriteRenderer;
    private Vector2 moveDirection;
    private bool grounded;
    private bool jump;
    private float jumpTime = 0;

    public float speed = 5;
    public float power = 5;

    [HideInInspector]
    private bool canMove = true;

    [Space(30)]
    [Header("------------------------------------")]

    [SerializeField]
    private SoundModule_Base playerSound;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            animator = GetComponent<Animator>();
            rigidbody2D = GetComponent<Rigidbody2D>();
            boxCollider2D = GetComponent<BoxCollider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    private void Start()
    {
        LevelObjectsManager.Instance.ONPlayerHurt += Death;
    }

    private void OnDisable()
    {
        LevelObjectsManager.Instance.ONPlayerHurt -= Death;
    }

    private void Update()
    {
        //땅위인지 체크
        grounded = GroundCheck();

        if (Time.deltaTime == 0)
            return;

        //이동
        Move(Input.GetAxisRaw("Horizontal") * speed);

        //점프
        Jump(power);
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    private void Move(float speed)
    {
        if (!canMove)
            return;
        rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
        if (speed < 0)
            spriteRenderer.flipX = true;
        else if (speed > 0)
            spriteRenderer.flipX = false;
        animator.SetBool("Walk", speed != 0);
    }

    private bool GroundCheck()
    {
        if (rigidbody2D.velocity.y <= 0.0001f)
        {
            Vector2 center = (Vector2)transform.position + (Vector2)boxCollider2D.offset + new Vector2(0, -0.1f);
            RaycastHit2D hit = Physics2D.BoxCast(center, boxCollider2D.size * new Vector2(0.9f,1), 0, Vector2.zero, 0, 1 << LayerMask.NameToLayer("Ground"));
            return hit;
        }

        return false;
    }

    private void Jump(float power)
    {
        if (animator.GetBool("Fall") && grounded)
            playerSound.Play("Land");
        animator.SetBool("Fall", !grounded);

        if (!canMove)
            return;

        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Jump");
            playerSound.Play("Jump");
            rigidbody2D.AddForce(Vector2.up * power);
        }

        //점프 끊기
        if (Input.GetKeyUp(KeyCode.Space) && rigidbody2D.velocity.y > 0)
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
    }

    public void Death()
    {
        if (!canMove)
            return;
        playerSound.Play("Death");
        animator.SetTrigger("Death");
        InGame.instance.soundModule.Stop();
        rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        canMove = false;
        StartCoroutine(RePlay());
    }

    IEnumerator RePlay()
    {
        InGame.playNum++;
        yield return new WaitForSeconds(2f);
        SceneMove.instance.SceneChange(SceneName.InGame);
        SceneMove.instance.CaptureScreenColor(new Color(0.5f, 0.5f, 0.5f), 1);
    }

    public void ThowCapsule(Vector3 from, Vector3 to, float time)
    {
        if (!canMove)
            StartCoroutine(Cor_ThowCapsule(from, to, time));
        InGame.instance.soundModule.Stop();
    }

    public IEnumerator Cor_ThowCapsule(Vector3 from, Vector3 to, float time)
    {
        canMove = false;
        
        rigidbody2D.velocity = Vector2.zero;
        transform.rotation = Quaternion.Euler(0f,180f,0f);

        InGame.instance.gameClear = true;
        if (from.x < to.x)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;

        animator.SetTrigger("Throw");
        yield return new WaitForSeconds(1f);
        
        playerSound.Play("Jump");

        spriteRenderer.sortingOrder = 2;

        var t = 0f;
        while (t < time)
        {
            yield return new WaitForFixedUpdate();
            float temp = Mathf.Min(1, t / time);
            transform.position = Vector3.Lerp(from, to, temp) + new Vector3(0f,Mathf.Sin(temp * Mathf.PI) * 5f ,0f);
            transform.rotation = Quaternion.Lerp(Quaternion.identity, Quaternion.Euler(0,0,180f), temp);
            t += Time.fixedDeltaTime;
        }

        spriteRenderer.sortingOrder = 0;

        rigidbody2D.bodyType = RigidbodyType2D.Static;


        // rigidbody2D.AddForce(Vector2.up * power*0.6f);
    }

}
