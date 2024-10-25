using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class BossEnemy : MonoBehaviour
{
    [SerializeField, Label("EnemyState")] EnemyState _enemyState;

    [SerializeField, Label("何ステージ目のオブジェクトか")] private int _enemyAssignment=1;

    [SerializeField,Label("HP")] private int _enemyHp;

    [SerializeField, Label("攻撃力")] private int _enemyPower;

    [SerializeField, Label("移動スピード")] private float _enemyMoveSpeed;

    [SerializeField, Label("弱点倍率")] private float _enemyWeekPointDamage=1.5f;

    [SerializeField, Label("攻撃間隔")] private float _enemyAttackSpan;


    void Awake()
    {
        //データ格納
        for(int i=0;i<_enemyState._stageEnemyDate.Length;i++)
        {
            if (_enemyState._stageEnemyDate[i]._stageNum == _enemyAssignment)
            {
                if (_enemyState._stageEnemyDate[i]._enemyTag == this.gameObject.tag)
                {
                    _enemyHp = _enemyState._stageEnemyDate[i]._enemyHp;
                    _enemyPower = _enemyState._stageEnemyDate[i]._enemyPower;
                    _enemyMoveSpeed= _enemyState._stageEnemyDate[i]._enemySpeed;
                    _enemyWeekPointDamage= _enemyState._stageEnemyDate[i]._weekPointDamage;
                    _enemyAttackSpan= _enemyState._stageEnemyDate[i]._attackSpan;
                    break;
                }
            }
        }
    }



    //衝突したオブジェクトの判定
    private void OnCollisionEnter2D(Collision2D collision)
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
            //Debug.Log("取得できなかった");
            return;
        }

        //攻撃済みか、済なら終了
        if (_objSt._afterDamage)
        {
            //Debug.Log("攻撃済");
            return;
        }

        //衝突オブジェクトのタグ取得
        string ColTag = collision.gameObject.tag;


        //アタックオブジェクトなのか判定
        for (int j = 0; j < _enemyState._setDamageClasses.Length; j++)
        {
            if (ColTag == _enemyState._setDamageClasses[j]._tag)
            {
                //アタックオブジェクトの場合、無効化されたタグが設定されていないか
                for (int i = 0; i < _enemyState._notPlayerAttackTag.Length; i++)
                {
                    if (ColTag == _enemyState._notPlayerAttackTag[i])
                    {
                        Debug.Log("ダメージ無効化オブジェクトです");
                        return;
                    }
                }
                //Debug.Log("無効化オブジェクトじゃないよ～");

                //ダメージ計算
                int ColDamage = _enemyState._setDamageClasses[j]._damage;
                ColDamage = (_enemyState._reflectionMagnification * _objSt._reflection) + ColDamage;
                Debug.Log("反射回数" + _objSt._reflection);

                //弱点判定がオンになっているか
                if (_objSt._weakness)
                {
                    //弱点ダメージ処理
                    _objSt._afterDamage = true;
                    WeekPointDamage(ColDamage);
                    return ;
                }
                //通常ダメージ処理
                _objSt._afterDamage = true;
                UsuallyDamage(ColDamage);
                return ;
            }
        }
        //Debug.Log("抜けた");
    }

    /// <summary>
    /// 弱点ダメージ処理
    /// </summary>
    /// <param name="_colDamage">通常ダメージ数値</param>
    void WeekPointDamage(int _colDamage)
    {
        int _weekDamage= Mathf.CeilToInt(_colDamage * 1.5f);
        Debug.Log("弱点ダメージ：" + _weekDamage);
        _enemyHp -= _weekDamage;
    }

    /// <summary>
    /// 通常ダメージ処理
    /// </summary>
    /// <param name="_colDamage">ダメージ数値</param>
    void UsuallyDamage(int _colDamage)
    {
        Debug.Log("通常ダメージ："+_colDamage);
        _enemyHp -= _colDamage;
    }
}