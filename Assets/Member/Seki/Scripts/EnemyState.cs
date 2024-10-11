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
        [Label("�^�O��"), Tag]
        public string _tag;

        [Label("�_���[�W"), Min(0)]
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

    [SerializeField, Label("�G��HP")] public int _enemyHp;

    [SerializeField, Label("�ړ����x"),Range(0.1f,100f)] public float _enemySpeed;

    [SerializeField, Label("�_���[�W�������^�O�ꗗ"), Tag]public string[] _invalidTag;

    [Label("�_���[�W�ݒ�")]public SetDamageClass[] _setDamageClasses;
}
