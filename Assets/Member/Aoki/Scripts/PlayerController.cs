using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public HpManager_aoki _hpmg;
    public float invincibleDuration = 1.5f;
    [SerializeField] private float speed = 0.3f;
    private bool isInvincible = false;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private bool isOnFloor = false;
    [SerializeField] private float timeOnFloor = 0f;
    [SerializeField] private const float tuchFloor = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;

        if (Input.GetKey(KeyCode.A))
        {
            position.x -= speed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            position.x += speed;
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

    ////���G����
    //public void PlayerTakeDamage(int damage)
    //{
    //    if (!isInvincible)
    //    {
    //        StartCoroutine(ActivateInvincibility());
    //        Debug.Log("���G");

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


