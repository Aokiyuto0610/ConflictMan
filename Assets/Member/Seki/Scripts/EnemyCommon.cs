using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Cysharp.Threading.Tasks;

public class EnemyCommon : MonoBehaviour
{
    [SerializeField, Label("EnemyState")] EnemyState _enemyState;

    [SerializeField, Label("���X�e�[�W�ڂ̃I�u�W�F�N�g��")] private int _enemyAssignment=1;

    [SerializeField, Label("�X�e�[�W�����̖ڂ�")] private int _enemyNum;

    [SerializeField,Label("HP")] private int _enemyHp;

    [SerializeField, Label("�U����")] private int _enemyPower;

    [SerializeField, Label("�ړ��X�s�[�h")] private float _enemyMoveSpeed;

    [SerializeField, Label("��_�{��")] private float _enemyWeekPointDamage=1.5f;

    [SerializeField, Label("�U���Ԋu")] private float _enemyAttackSpan;

    //span�v���p
    private float _attackSpanTime = 0;

    //�A�^�b�N����
    private bool _attacking=false;

    [SerializeField] private EnemyAttack _enemyAttack;

    [SerializeField] private GameObject _enemySp;

    [SerializeField] private GameObject _enemyAttackObj;

    [SerializeField] private EnemyMove _enemyMove;

    [SerializeField] Animator _enemyAnimator;

    [SerializeField] EnemyHpBar _hpBar;


    void Awake()
    {
        //�J�����p
        Application.targetFrameRate = 60;

        //�f�[�^�i�[
        for (int i=0;i<_enemyState._stageEnemyDate.Length;i++)
        {
            //�X�e�[�W�i���o�[��v
            if (_enemyState._stageEnemyDate[i]._stageNum == _enemyAssignment)
            {
                //�^�O��v
                if (_enemyState._stageEnemyDate[i]._enemyTag == this.gameObject.tag)
                {
                    //�i���o�[��v
                    if (_enemyState._stageEnemyDate[i]._enemyNum == _enemyNum ||this.gameObject.tag=="BossEnemy" )
                    {
                        //�i�[��
                        _enemyHp = _enemyState._stageEnemyDate[i]._enemyHp;
                        _enemyPower = _enemyState._stageEnemyDate[i]._enemyPower;
                        _enemyMove._moveSpeed = _enemyState._stageEnemyDate[i]._enemySpeed;
                        _enemyWeekPointDamage = _enemyState._stageEnemyDate[i]._weekPointDamage;
                        //_enemyAttackSpan = _enemyState._stageEnemyDate[i]._attackSpan + 1;
                        _enemyAttack.SetAttackDamage(_enemyState._stageEnemyDate[i]._enemyPower);
                        _hpBar.SetEnemyHp(_enemyHp);
                        break;
                    }
                }
            }
        }

        //null�`�F�b�N
        /*
        if(_enemyAnimator == null)
        {
            Debug.LogError("Animator���Ȃ�");
        }
        */
    }

    async void Update()
    {
        _attackSpanTime += Time.deltaTime;
        if(_attackSpanTime >= _enemyAttackSpan)
        {
            await EnemyAttackMove(1);
            _attackSpanTime = 0;
        }

        //�ړ���
        //this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x+0.005f, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
    }


    //�Փ˂����I�u�W�F�N�g�̔���
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("�Փ�");

        PlayerTest _objSt;

        //�I�u�W�F�N�g���i�[�N���X�擾�Ȃ��Ȃ�I��
        if (collision.gameObject.GetComponent<PlayerTest>())
        {
            //Debug.Log("�擾");
            _objSt = collision.gameObject.GetComponent<PlayerTest>();
        }
        else
        {
            //Debug.Log("�擾�ł��Ȃ�����");
            return;
        }

        //�U���ς݂��A�ςȂ�I��
        //if (_objSt._afterDamage)
        //{
        //    //Debug.Log("�U����");
        //    return;
        //}

        //�Փ˃I�u�W�F�N�g�̃^�O�擾
        string ColTag = collision.gameObject.tag;


        //�A�^�b�N�I�u�W�F�N�g�Ȃ̂�����
        for (int j = 0; j < _enemyState._setDamageClasses.Length; j++)
        {
            if (ColTag == _enemyState._setDamageClasses[j]._tag)
            {
                //�A�^�b�N�I�u�W�F�N�g�̏ꍇ�A���������ꂽ�^�O���ݒ肳��Ă��Ȃ���
                for (int i = 0; i < _enemyState._notPlayerAttackTag.Length; i++)
                {
                    if (ColTag == _enemyState._notPlayerAttackTag[i])
                    {
                        Debug.Log("�_���[�W�������I�u�W�F�N�g�ł�");
                        return;
                    }
                }
                //Debug.Log("�������I�u�W�F�N�g����Ȃ���`");

                //�_���[�W�v�Z
                int ColDamage = _enemyState._setDamageClasses[j]._damage;
                ColDamage = (_enemyState._reflectionMagnification * _objSt._reflection) + ColDamage;
                Debug.Log("���ˉ�" + _objSt._reflection);

                //��_���肪�I���ɂȂ��Ă��邩
                if (_objSt._weakness)
                {
                    //��_�_���[�W����
                    _objSt._afterDamage = true;
                    WeekPointDamage(ColDamage);
                    return ;
                }
                else
                {
                    //�ʏ�_���[�W����
                    _objSt._afterDamage = true;
                    UsuallyDamage(ColDamage);
                    return ;
                }
            }
        }
        //Debug.Log("������");
    }

    /// <summary>
    /// ��_�_���[�W����
    /// </summary>
    /// <param name="_colDamage">�ʏ�_���[�W���l</param>
    async void WeekPointDamage(int _colDamage)
    {
        //�ʏ�_���[�W��1.5�{�Ő؂�グ�����l���i�[
        int _weekDamage= Mathf.CeilToInt(_colDamage * 1.5f);
        Debug.Log("��_�_���[�W�F" + _weekDamage);
        //����
        _enemyHp -= _weekDamage;
        //HP�o�[����
        _hpBar.SetNowHp(_enemyHp);
        //Deth����
        if (_enemyHp <= 0)
        {
            await EnemySlain();
        }
    }

    /// <summary>
    /// �ʏ�_���[�W����
    /// </summary>
    /// <param name="_colDamage">�_���[�W���l</param>
    async void UsuallyDamage(int _colDamage)
    {
        Debug.Log("�ʏ�_���[�W�F"+_colDamage);
        _enemyHp -= _colDamage;
        //HP�o�[����
        _hpBar.SetNowHp(_enemyHp);

        if (_enemyHp <= 0)
        {
            await EnemySlain();
        }
    }

    /// <summary>
    /// Deth����
    /// </summary>
    /// <returns></returns>
    async UniTask EnemySlain()
    {
        for(int i=0; i < 2; i++)
        {
            _enemySp.SetActive(false);
            await UniTask.Delay(100);
            _enemySp.SetActive(true);
            await UniTask.Delay(100);
        }
        _enemyState.EnemySlain();
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// �A�^�b�N
    /// </summary>
    /// <param name="speed"></param>
    /// <returns></returns>
    [Button]
    async UniTask EnemyAttackMove(int speed=1)
    {
        if (!_attacking)
        {
            _attacking = true;
            _enemyAttackObj.SetActive(true);
            _enemyAnimator.SetFloat("AttackSpeed", speed);
            _enemyAnimator.SetTrigger("Attack");
            await UniTask.Delay(1000 / speed);
            _enemyAttackObj.SetActive(false);
            _enemyAnimator.Play("Idle");
            _attacking = false;
        }
        else
        {
            return;
        }
    }

}