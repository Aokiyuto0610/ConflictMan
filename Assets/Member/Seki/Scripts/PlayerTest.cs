using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    [SerializeField, Label("��_�͈͂ɓ����Ă��邩")] public bool _weakness=false;

    [SerializeField, Label("���͈͂ɓ������̂�")] public bool _shield = false;

    //�U���J�n����false�ɏ��������āI�I�I
    [SerializeField, Label("�_���[�W��^�����ォ")] public bool _afterDamage=false;

    //���ˉ񐔁A��~���ɏ������I
    public int _reflection = 0;

    //�f�o�b�N�p
    [SerializeField] public int _playerHp = 9999;

    /// <summary>
    /// �v���C���[��HP�����
    /// </summary>
    /// <param name="Damage"></param>
    public void ReceivedDamage(int Damage)
    {
        /*
            �v���C���[��HP����鏈��
         */

        //�f�o�b�O�p
        _playerHp-=Damage;
    }

}