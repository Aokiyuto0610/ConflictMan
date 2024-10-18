using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class BossEnemy : MonoBehaviour
{
    [SerializeField, Label("EnemyState")] EnemyState _enemyState;

    [SerializeField, Label("���X�e�[�W�ڂ̃I�u�W�F�N�g��")] private int _enemyAssignment;

    [SerializeField,Label("HP")] private int _enemyHp;

    [SerializeField, Label("�U����")] private int _enemyPower;

    [SerializeField, Label("�ړ��X�s�[�h")] private float _enemyMoveSpeed;

    [SerializeField, Label("��_�{��")] private float _enemyWeekPointDamage;

    [SerializeField, Label("�U���Ԋu")] private float _enemyAttackSpan;



    private void Start()
    {
        
    }

    //�Փ˂����I�u�W�F�N�g�̔���
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�Փ˃I�u�W�F�N�g�̃^�O�擾
        string ColTag = collision.gameObject.tag;


        //�A�^�b�N�I�u�W�F�N�g�Ȃ̂�����
        for (int j = 0; j < _enemyState._setDamageClasses.Length; j++)
        {
            if (ColTag == _enemyState._setDamageClasses[j]._tag)
            {
                //�A�^�b�N�I�u�W�F�N�g�̏ꍇ�A���������ꂽ�^�O���ݒ肳��Ă��Ȃ���
                for (int i = 0; i < _enemyState._invalidTag.Length; i++)
                {
                    if (ColTag == _enemyState._invalidTag[i])
                    {
                        Debug.Log("�_���[�W�������I�u�W�F�N�g�ł�");
                        return;
                    }
                }
                Debug.Log("�������I�u�W�F�N�g����Ȃ���`");

                //��_���肪�I���ɂȂ��Ă��邩
                if (collision.gameObject.GetComponent<AttackObj>()._weakPoint)
                {
                    /*
                     �����Ɏ�_�_���[�W����������
                     */
                    Debug.Log("��_�U���I�I");
                    return ;
                }

                /*
                 �����ɒʏ�_���[�W����������
                 */
            }
        }
    }
}