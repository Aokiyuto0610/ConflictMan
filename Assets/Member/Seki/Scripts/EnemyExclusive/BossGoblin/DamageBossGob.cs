using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBossGob : MonoBehaviour
{
    [SerializeField] EnemyState _state;

    [SerializeField] BossGoblin _bossGob;

    /// <summary>
    /// ダメージ用
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("衝突");

        PlayerTest _objSt;

        //オブジェクト情報格納クラス取得ないなら終了
        if (collision.gameObject.GetComponent<PlayerTest>())
        {
            //Debug.Log("取得");
            _objSt = collision.gameObject.GetComponent<PlayerTest>();
        }
        else
        {
            return;
        }

        //攻撃中かどうか、違うなら終了
        if (_objSt._afterDamage)
        {
            _objSt._afterDamage = false;
        }
        else
        {
            return;
        }

        //衝突オブジェクトのタグ取得
        string ColTag = collision.gameObject.tag;


        //アタックオブジェクトなのか判定
        for (int j = 0; j < _state._setDamageClasses.Length; j++)
        {
            if (ColTag == _state._setDamageClasses[j]._tag)
            {
                //アタックオブジェクトの場合、無効化されたタグが設定されていないか
                for (int i = 0; i < _state._notPlayerAttackTag.Length; i++)
                {
                    if (ColTag == _state._notPlayerAttackTag[i])
                    {
                        Debug.Log("ダメージ無効化オブジェクトです");
                        return;
                    }
                }

                //ダメージ計算
                int ColDamage = _state._setDamageClasses[j]._damage;
                ColDamage = (_state._reflectionMagnification * _objSt._reflection) + ColDamage;
                Debug.Log("反射回数" + _objSt._reflection);

                //弱点判定がオンになっているか
                if (_objSt._weakness)
                {
                    //弱点ダメージ処理
                    _objSt._afterDamage = true;
                    _bossGob.WeekPointDamage(ColDamage);
                    return;
                }
                else
                {
                    //通常ダメージ処理
                    _objSt._afterDamage = true;
                    _bossGob.UsuallyDamage(ColDamage);
                    return;
                }
            }
        }
    }
}