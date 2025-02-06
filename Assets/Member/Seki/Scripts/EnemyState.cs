using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;

public class EnemyState : MonoBehaviour
{
    //ダメージ情報
    [Serializable]
    public class SetDamageClass
    {
        //対象タグ
        [Tag]
        public string _tag;

        //与えるダメージ
        [Min(1)]
        public int _damage = 1;
    }

    //敵情報
    [Serializable]
    public class StageEnemyDate
    {
        [Label("Stage番号")]
        public int _stageNum = 1;

        [Label("そのステージで何体目か")] 
        public int _enemyNum;

        [Label("タグ指定"),Tag]
        public string _enemyTag;

        [Label("HP"),Min(1)]
        public int _enemyHp = 1;

        [Label("攻撃力")]
        public int _enemyPower = 1;

        [Label("移動スピード"), Range(0.1f, 99f)] 
        public float _enemySpeed = 1;

        [Label("弱点倍率")]
        public float _weekPointDamage=1;

        [Label("攻撃感覚")]
        float[] _enemyAttackSpan;

        [Label("攻撃種類")]
        int[] _enemyAttackType;

        public float GetEnemyAttackSpan(int i)
        {
            return _enemyAttackSpan[i];
        }

        public int GetEnemyAttackSpanLegth()
        {
            return _enemyAttackSpan.Length;
        }

        public int GetEnemyAttackType(int i)
        { 
            return _enemyAttackType[i];
        }

        public int GetEnemyAttackTypeLegth()
        {
            return _enemyAttackType.Length;
        }
    }


    [Label("敵の攻撃が当たらないオブジェクトタグ"), Tag] public string[] _notEnemyAttackTag;

    [Label("敵へのダメージを無効化するプレイヤーのタグ"), Tag]public string[] _notPlayerAttackTag;

    [Label("オブジェクトダメージ設定")]public SetDamageClass[] _setDamageClasses;

    [Label("エネミーステータス設定")]public StageEnemyDate[] _stageEnemyDate;

    [Label("反射した回数に応じた変化数値")] public int _reflectionMagnification = 20;

    [Label("折り返し地点のタグ"), Tag] public string _turnPointTag;

    [Label("このゲーム上に存在する敵数")] public int _enemyTotal;

    [Label("倒された敵数")] public int _enemySlainNum = 0;

    [Label("リザルトScene"), Scene] public string _resultScene;

    [Label("スタート済")] public bool _gameStart = false;

    private void Start()
    {
        Debug.Log(_stageEnemyDate.Length);
        _enemyTotal=_stageEnemyDate.Length;
    }

    //敵が倒された際に呼び出し
    public void EnemySlain()
    {
        //討伐数増加
        _enemySlainNum++;

        //全て討伐できたか
        if (_enemySlainNum >= _enemyTotal)
        {
            //リザルトに遷移
            FadeManager.Instance.LoadScene(_resultScene, 1f);
        }
    }
}
