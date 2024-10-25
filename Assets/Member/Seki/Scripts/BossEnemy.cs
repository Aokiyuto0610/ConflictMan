using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class BossEnemy : MonoBehaviour
{
    [SerializeField, Label("EnemyState")] EnemyState _enemyState;

    [SerializeField, Label("���X�e�[�W�ڂ̃I�u�W�F�N�g��")] private int _enemyAssignment=1;

    [SerializeField,Label("HP")] private int _enemyHp;

    [SerializeField, Label("�U����")] private int _enemyPower;

    [SerializeField, Label("�ړ��X�s�[�h")] private float _enemyMoveSpeed;

    [SerializeField, Label("��_�{��")] private float _enemyWeekPointDamage=1.5f;

    [SerializeField, Label("�U���Ԋu")] private float _enemyAttackSpan;


    void Awake()
    {
        //�f�[�^�i�[
        for(int i=0;i<_enemyState._stageEnemyDate.Length;i++)
        {
            if (_enemyState._stageEnemyDate[i]._stageNum == _enemyAssignment)
            {
                if (_enemyState._stageEnemyDate[i]._enemyTag == this.gameObject.tag)
                {
                    _enemyHp = _enemyState._stageEnemyDate[i]._enemyHp;
                    _enemyPower = _enemyState._stageEnemyDate[i]._enemyPower;
                    _enemyMoveSpeed= _enemyState._stageEnemyDate[i]._enemySpeed;
                    _enemyWeekPointDamage= _enemyState._stageEnemyDate[i]._weekPointDamage;
                    _enemyAttackSpan= _enemyState._stageEnemyDate[i]._attackSpan;
                    break;
                }
            }
        }
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
        if (_objSt._afterDamage)
        {
            //Debug.Log("�U����");
            return;
        }

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
                //�ʏ�_���[�W����
                _objSt._afterDamage = true;
                UsuallyDamage(ColDamage);
                return ;
            }
        }
        //Debug.Log("������");
    }

    /// <summary>
    /// ��_�_���[�W����
    /// </summary>
    /// <param name="_colDamage">�ʏ�_���[�W���l</param>
    void WeekPointDamage(int _colDamage)
    {
        int _weekDamage= Mathf.CeilToInt(_colDamage * 1.5f);
        Debug.Log("��_�_���[�W�F" + _weekDamage);
        _enemyHp -= _weekDamage;
    }

    /// <summary>
    /// �ʏ�_���[�W����
    /// </summary>
    /// <param name="_colDamage">�_���[�W���l</param>
    void UsuallyDamage(int _colDamage)
    {
        Debug.Log("�ʏ�_���[�W�F"+_colDamage);
        _enemyHp -= _colDamage;
    }
}