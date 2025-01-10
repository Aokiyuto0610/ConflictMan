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
        Debug.Log("攻撃が発生しました！");
        // 攻撃の処理をここに記述
    }

    // 地面に触れたときの処理
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isTouchingFloor = true;
        }
    }

    // 地面から離れたときの処理
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isTouchingFloor = false;
        }
    }
}
