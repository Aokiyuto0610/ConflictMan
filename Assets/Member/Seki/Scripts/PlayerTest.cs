using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    [SerializeField, Label("弱点範囲に入っているか")] public bool _weakness=false;

    [SerializeField, Label("盾範囲に入ったのか")] public bool _shield = false;

    //連続衝突回避用
    //攻撃開始時にfalseに初期化して！！！
    [SerializeField, Label("敵ダメージを与えた後か")] public bool _afterDamage=false;

    //反射回数、停止時に初期化！
    public int _reflection = 0;

    //デバック用
    [SerializeField, OnValueChanged(nameof(OnValueChanged))] private int _playerHp = 9999;


    //デバック用
    private void OnValueChanged()
    {
        //Debug.Log("テストプレイヤーHP"+_playerHp);
    }

    /// <summary>
    /// プレイヤーのHPを削る
    /// </summary>
    /// <param name="Damage"></param>
    public void ReceivedDamage(int Damage)
    {

        /*
            プレイヤーのHPを削る処理
         */

        //デバッグ用、上を書いたらコメントアウト
        _playerHp-=Damage;
        Debug.Log(_playerHp);
    }
}