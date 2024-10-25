using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �p�r�F�G�̎�_�͈̓I�u�W�F�N�g�ɃA�^�b�`���āA�������Ă����I�u�W�F�N�g�̎�_����p�ϐ���ύX����
/// </summary>

public class WeakPoint : MonoBehaviour
{

    /// <summary>
    /// �͈͂ɓ��������_�����true�ɂ���
    /// </summary>
    /// <param name="collision">collider</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerTest>())
        {
            PlayerTest st = collision.gameObject.GetComponent<PlayerTest>();
            st._weakness = true;
        }
        else
        {
            Debug.Log("�i�[�ϐ����A�^�b�`����ĂȂ�");
        }
    }

    /// <summary>
    /// �͈͂���o�����_�����false�ɂ���
    /// </summary>
    /// <param name="collision">collider</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerTest>())
        {
            PlayerTest st = collision.gameObject.GetComponent<PlayerTest>();
            st._weakness = false;
        }
        else
        {
            Debug.Log("�i�[�ϐ����A�^�b�`����ĂȂ�");
        }
    }
}
