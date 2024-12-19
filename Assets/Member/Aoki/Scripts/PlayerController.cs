using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public HpManager_aoki _hpmg;
    [SerializeField] private float speed;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private bool isOnFloor = false;
    private bool isFacingRight = true;

    private Rigidbody2D rb;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 position = transform.position;

        // 横方向の入力取得
        float inputX = Input.GetAxisRaw("Horizontal");

        // アニメーションの状態を設定
        anim.SetBool("PlayerMove", inputX != 0);

        if (inputX < 0)
        {
            position.x -= speed * Time.deltaTime;
            if (isFacingRight)
            {
                Flip();
            }
        }
        else if (inputX > 0)
        {
            position.x += speed * Time.deltaTime;
            if (!isFacingRight)
            {
                Flip();
            }
        }

        transform.position = position;
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            isOnFloor = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            isOnFloor = false;
        }
    }
}
