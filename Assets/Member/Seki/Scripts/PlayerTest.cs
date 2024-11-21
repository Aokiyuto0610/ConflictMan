using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    [SerializeField, Label("弱点範囲に入っているか")] public bool _weakness=false;

    [SerializeField, Label("盾範囲に入ったのか")] public bool _shield = false;

    //連続衝突回避用
    //攻撃開始時にfalseに初期化して！！！
    [SerializeField, Label("敵ダメージを与えた後か")] public bool _afterDamage=false;

    //反射回数、停止時に初期化！
    public int _reflection = 0;

    //デバック用
    [SerializeField, OnValueChanged(nameof(OnValueChanged))] private int _playerHp = 9999;


    //デバック用
    private void OnValueChanged()
    {
        //Debug.Log("テストプレイヤーHP"+_playerHp);
    }

    /// <summary>
    /// プレイヤーのHPを削る
    /// </summary>
    /// <param name="Damage"></param>
    public void ReceivedDamage(int Damage)
    {

        /*
            プレイヤーのHPを削る処理
         */
        PlayerTakeDamage(Damage);

        //デバッグ用、上を書いたらコメントアウト
        _playerHp-=Damage;
        Debug.Log("PlayerHP"+_playerHp);
    }

    //青木追記
    private bool isInvincible = false;
    public float invincibleDuration = 1.5f;
    [SerializeField] private SpriteRenderer spriteRenderer;

    //無敵処理
    public void PlayerTakeDamage(int damage)
    {
        if (!isInvincible)
        {
            StartCoroutine(ActivateInvincibility());
            Debug.Log("無敵");

            //_hpmg.TakeDamage();
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("EnemyAttack") && !isInvincible)
        {
            ReceivedDamage(1);
        }
    }
}