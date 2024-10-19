using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;

public class EnemyState : MonoBehaviour
{
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

    [Serializable]
    public class StageEnemyDate
    {
        [Label("Stage番号")]
        public int _stageNum = 1;

        [Label("タグ指定"),Tag]
        public string _enemyTag;

        [Label("HP"),Min(1)]
        public int _enemyHp = 1;

        [Label("攻撃力")]
        public int _enemyPower = 1;

        [Label("移動スピード"), Range(0.1f, 100f)] 
        public float _enemySpeed = 1;

        [Label("弱点倍率")]
        public float _weekPointDamage=1;

        public float _attackSpan=1;
    }

    [SerializeField, Label("ダメージ無効化タグ一覧"), Tag]public string[] _invalidTag;

    [Label("オブジェクトダメージ設定")]public SetDamageClass[] _setDamageClasses;

    [Label("エネミーステータス設定")]public StageEnemyDate[] _stageEnemyDate;
}
