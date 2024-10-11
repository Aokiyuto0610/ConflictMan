using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class BossEnemy : MonoBehaviour
{
    [SerializeField, Label("EnemyState")] EnemyState _enemyState;


    //衝突したオブジェクトの判定
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //衝突オブジェクトのタグ取得
        string ColTag = collision.gameObject.tag;

        //無効化されたタグが設定されていないか
        for(int i =0; i<_enemyState._invalidTag.Length; i++)
        {
            if (ColTag == _enemyState._invalidTag[i])
            {
                Debug.Log("ダメージ無効化オブジェクトです");
                return;
            }
        }
        Debug.Log("無効化オブジェクトじゃないよ～");
    }
}