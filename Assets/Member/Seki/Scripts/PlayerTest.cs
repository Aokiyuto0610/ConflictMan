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

    [SerializeField, Label("ハート")]
    private List<GameObject> hearts;

    //反射回数、停止時に初期化！
    public int _reflection = 0;

    //デバック用
    [SerializeField, OnValueChanged(nameof(OnValueChanged))] private int _playerHp;

    [SerializeField, Scene] public string _LoseScene;


    private void Start()
    {
        // プレイヤーHPとハートの初期同期
        UpdateHeartDisplay();
    }

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

        if(!isInvincible)
        {
            PlayerTakeDamage(Damage);
            _afterDamage = false;

            _playerHp -= Damage;
            UpdateHeartDisplay(); // ハート表示を更新

            Debug.Log("PlayerHP: " + _playerHp);

            if (_playerHp <= 0)
            {
                FadeManager.Instance.LoadScene(_LoseScene, 1f);
            }
        }
        //PlayerTakeDamage(Damage);
        //_afterDamage = false;

        ////デバッグ用、上を書いたらコメントアウト
        //_playerHp-=Damage;
        //Debug.Log("PlayerHP"+_playerHp);

        //if(_playerHp <= 0)
        //{
        //    FadeManager.Instance.LoadScene(_LoseScene,1f);
        //}
    }

    //青木追記
    private bool isInvincible = false;
    public float invincibleDuration;
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

        float blinkInterval = 0.1f;
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

    /// <summary>
    /// ハート表示を更新
    /// </summary>
    private void UpdateHeartDisplay()
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            hearts[i].SetActive(i < _playerHp);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            _afterDamage = false; // Floorに触れたのでfalse
            Debug.Log("a");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            _afterDamage = true; // Floorから離れたのでtrue
        }
    }
}