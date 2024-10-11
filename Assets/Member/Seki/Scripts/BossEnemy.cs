using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class BossEnemy : MonoBehaviour
{
    [SerializeField, Label("EnemyState")] EnemyState _enemyState;


    //�Փ˂����I�u�W�F�N�g�̔���
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�Փ˃I�u�W�F�N�g�̃^�O�擾
        string ColTag = collision.gameObject.tag;

        //���������ꂽ�^�O���ݒ肳��Ă��Ȃ���
        for(int i =0; i<_enemyState._invalidTag.Length; i++)
        {
            if (ColTag == _enemyState._invalidTag[i])
            {
                Debug.Log("�_���[�W�������I�u�W�F�N�g�ł�");
                return;
            }
        }
        Debug.Log("�������I�u�W�F�N�g����Ȃ���`");
    }
}