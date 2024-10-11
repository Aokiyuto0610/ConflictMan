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
        [Label("タグ名"), Tag]
        public string _tag;

        [Label("ダメージ"), Min(0)]
        public int _damage;

        public string GetTag()
        {
            return _tag;
        }

        public int GetDamage()
        {
            return _damage;
        }

    }

    [SerializeField, Label("敵のHP")] public int _enemyHp;

    [SerializeField, Label("移動速度"),Range(0.1f,100f)] public float _enemySpeed;

    [SerializeField, Label("ダメージ無効化タグ一覧"), Tag]public string[] _invalidTag;

    [Label("ダメージ設定")]public SetDamageClass[] _setDamageClasses;
}
