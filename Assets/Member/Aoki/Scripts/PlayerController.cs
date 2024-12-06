using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public HpManager_aoki _hpmg;
    public float invincibleDuration;
    [SerializeField] 
    private float speed;
    [SerializeField] 
    private SpriteRenderer spriteRenderer;

    private bool isOnFloor = false;
    [SerializeField]
    private float timeOnFloor;
    [SerializeField]
    private const float tuchFloor = 0.5f;

    private bool isFacingRight = true;
    private bool isMoving = false;
    private bool isNaturallMoving = false;

    private Rigidbody2D rb;

    [SerializeField]
    private float naturalMovement;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isNaturallMoving = rb.velocity.magnitude > naturalMovement;
        if (isNaturallMoving) return;

        Vector2 position = transform.position;

        if (Input.GetKey(KeyCode.A))
        {
            position.x -= speed;
            if(isFacingRight)
            {
                Flip();
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            position.x += speed;
            if(!isFacingRight)
            {
                Flip();
            }
        }

        transform.position = position;

        if(isOnFloor)
        {
            timeOnFloor += Time.deltaTime;
            if(timeOnFloor >= tuchFloor)
            {
                float zRotation = transform.rotation.eulerAngles.z;
                if(zRotation > 1f &&zRotation < 359f)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }
        }
        else
        {
            timeOnFloor = 0f;
        }
    }

    ////–³“Gˆ—
    //public void PlayerTakeDamage(int damage)
    //{
    //    if (!isInvincible)
    //    {
    //        StartCoroutine(ActivateInvincibility());
    //        Debug.Log("–³“G");

    //        //_hpmg.TakeDamage();
    //    }
    //}

    //private IEnumerator ActivateInvincibility()
    //{
    //    isInvincible = true;

    //    float blinkInterval = 0.2f;
    //    for (float i = 0; i < invincibleDuration; i += blinkInterval)
    //    {
    //        spriteRenderer.enabled = !spriteRenderer.enabled;
    //        yield return new WaitForSeconds(blinkInterval);
    //    }
    //    spriteRenderer.enabled = true;

    //    isInvincible = false;
    //}

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Floor"))
        {
            isOnFloor = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Floor"))
        {
            isOnFloor = false;
        }
    }
}


