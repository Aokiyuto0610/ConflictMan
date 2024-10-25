using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 用途：敵の弱点範囲オブジェクトにアタッチして、当たってきたオブジェクトの弱点判定用変数を変更する
/// </summary>

public class WeakPoint : MonoBehaviour
{

    /// <summary>
    /// 範囲に入ったら弱点判定をtrueにする
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
            Debug.Log("格納変数がアタッチされてない");
        }
    }

    /// <summary>
    /// 範囲から出たら弱点判定をfalseにする
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
            Debug.Log("格納変数がアタッチされてない");
        }
    }
}
