using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConflictItem : MonoBehaviour
{
    private bool isTouchingFloor = false;

    void Update()
    {

    }

    void Attack()
    {
        Debug.Log("�U�����������܂����I");
        // �U���̏����������ɋL�q
    }

    // �n�ʂɐG�ꂽ�Ƃ��̏���
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isTouchingFloor = true;
        }
    }

    // �n�ʂ��痣�ꂽ�Ƃ��̏���
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isTouchingFloor = false;
        }
    }
}
