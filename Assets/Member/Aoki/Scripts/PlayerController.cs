using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public HpManager _hpmg;
    public float invincibleDuration = 1.5f;
    private float speed = 0.3f;
    private bool isInvincible = false;
    private SpriteRenderer spriteRenderer;

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
    }

    //ñ≥ìGèàóù
    public void PlayerTakeDamage(int damage)
    {
        if (!isInvincible)
        {
            StartCoroutine(ActivateInvincibility());
            _hpmg.TakeDamage();
        }
    }

    private IEnumerator ActivateInvincibility()
    {
        isInvincible = true;

        float blinkInterval = 0.2f;
        for (float i = 0; i < invincibleDuration; i += blinkInterval)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
        }
        spriteRenderer.enabled = true;

        isInvincible = false;
    }

}


