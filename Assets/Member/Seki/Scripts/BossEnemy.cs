using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class BossEnemy : MonoBehaviour
{
    [SerializeField, Label("EnemyState")] EnemyState _enemyState;

    [SerializeField, Label("何ステージ目のオブジェクトか")] private int _enemyAssignment;

    [SerializeField,Label("HP")] private int _enemyHp;

    [SerializeField, Label("攻撃力")] private int _enemyPower;

    [SerializeField, Label("移動スピード")] private float _enemyMoveSpeed;

    [SerializeField, Label("弱点倍率")] private float _enemyWeekPointDamage;

    [SerializeField, Label("攻撃間隔")] private float _enemyAttackSpan;



    private void Start()
    {
        
    }

    //衝突したオブジェクトの判定
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //衝突オブジェクトのタグ取得
        string ColTag = collision.gameObject.tag;


        //アタックオブジェクトなのか判定
        for (int j = 0; j < _enemyState._setDamageClasses.Length; j++)
        {
            if (ColTag == _enemyState._setDamageClasses[j]._tag)
            {
                //アタックオブジェクトの場合、無効化されたタグが設定されていないか
                for (int i = 0; i < _enemyState._invalidTag.Length; i++)
                {
                    if (ColTag == _enemyState._invalidTag[i])
                    {
                        Debug.Log("ダメージ無効化オブジェクトです");
                        return;
                    }
                }
                Debug.Log("無効化オブジェクトじゃないよ～");

                //弱点判定がオンになっているか
                if (collision.gameObject.GetComponent<AttackObj>()._weakPoint)
                {
                    /*
                     ここに弱点ダメージ処理を入れる
                     */
                    Debug.Log("弱点攻撃！！");
                    return ;
                }

                /*
                 ここに通常ダメージ処理を入れる
                 */
            }
        }
    }
}