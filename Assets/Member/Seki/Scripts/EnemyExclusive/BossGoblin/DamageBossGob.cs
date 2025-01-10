using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBossGob : MonoBehaviour
{
    [SerializeField] EnemyState _state;

    [SerializeField] BossGoblin _bossGob;

    /// <summary>
    /// �_���[�W�p
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
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
            return;
        }

        //�U�������ǂ����A�Ⴄ�Ȃ�I��
        if (_objSt._afterDamage)
        {
            _objSt._afterDamage = false;
        }
        else
        {
            return;
        }

        //�Փ˃I�u�W�F�N�g�̃^�O�擾
        string ColTag = collision.gameObject.tag;


        //�A�^�b�N�I�u�W�F�N�g�Ȃ̂�����
        for (int j = 0; j < _state._setDamageClasses.Length; j++)
        {
            if (ColTag == _state._setDamageClasses[j]._tag)
            {
                //�A�^�b�N�I�u�W�F�N�g�̏ꍇ�A���������ꂽ�^�O���ݒ肳��Ă��Ȃ���
                for (int i = 0; i < _state._notPlayerAttackTag.Length; i++)
                {
                    if (ColTag == _state._notPlayerAttackTag[i])
                    {
                        Debug.Log("�_���[�W�������I�u�W�F�N�g�ł�");
                        return;
                    }
                }

                //�_���[�W�v�Z
                int ColDamage = _state._setDamageClasses[j]._damage;
                ColDamage = (_state._reflectionMagnification * _objSt._reflection) + ColDamage;
                Debug.Log("���ˉ�" + _objSt._reflection);

                //��_���肪�I���ɂȂ��Ă��邩
                if (_objSt._weakness)
                {
                    //��_�_���[�W����
                    _objSt._afterDamage = true;
                    _bossGob.WeekPointDamage(ColDamage);
                    return;
                }
                else
                {
                    //�ʏ�_���[�W����
                    _objSt._afterDamage = true;
                    _bossGob.UsuallyDamage(ColDamage);
                    return;
                }
            }
        }
    }
}