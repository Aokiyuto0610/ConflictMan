using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    [SerializeField, Label("��_�͈͂ɓ����Ă��邩")] public bool _weakness=false;

    [SerializeField, Label("���͈͂ɓ������̂�")] public bool _shield = false;

    //�A���Փˉ��p
    //�U���J�n����false�ɏ��������āI�I�I
    [SerializeField, Label("�G�_���[�W��^�����ォ")] public bool _afterDamage=false;

    [SerializeField, Label("�n�[�g")]
    private List<GameObject> hearts;

    //���ˉ񐔁A��~���ɏ������I
    public int _reflection = 0;

    //�f�o�b�N�p
    [SerializeField, OnValueChanged(nameof(OnValueChanged))] private int _playerHp;

    [SerializeField, Scene] public string _LoseScene;


    private void Start()
    {
        // �v���C���[HP�ƃn�[�g�̏�������
        UpdateHeartDisplay();
    }

    //�f�o�b�N�p
    private void OnValueChanged()
    {
        //Debug.Log("�e�X�g�v���C���[HP"+_playerHp);
    }

    /// <summary>
    /// �v���C���[��HP�����
    /// </summary>
    /// <param name="Damage"></param>
    public void ReceivedDamage(int Damage)
    {

        /*
            �v���C���[��HP����鏈��
         */

        if(!isInvincible)
        {
            PlayerTakeDamage(Damage);
            _afterDamage = false;

            _playerHp -= Damage;
            UpdateHeartDisplay(); // �n�[�g�\�����X�V

            Debug.Log("PlayerHP: " + _playerHp);

            if (_playerHp <= 0)
            {
                FadeManager.Instance.LoadScene(_LoseScene, 1f);
            }
        }
        //PlayerTakeDamage(Damage);
        //_afterDamage = false;

        ////�f�o�b�O�p�A�����������R�����g�A�E�g
        //_playerHp-=Damage;
        //Debug.Log("PlayerHP"+_playerHp);

        //if(_playerHp <= 0)
        //{
        //    FadeManager.Instance.LoadScene(_LoseScene,1f);
        //}
    }

    //�ؒǋL
    private bool isInvincible = false;
    public float invincibleDuration;
    [SerializeField] private SpriteRenderer spriteRenderer;


    //���G����
    public void PlayerTakeDamage(int damage)
    {
        if (!isInvincible)
        {
            StartCoroutine(ActivateInvincibility());
            Debug.Log("���G");

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
    /// �n�[�g�\�����X�V
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
            _afterDamage = false; // Floor�ɐG�ꂽ�̂�false
            Debug.Log("a");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            _afterDamage = true; // Floor���痣�ꂽ�̂�true
        }
    }
}