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
        //�Ώۃ^�O
        [Tag]
        public string _tag;

        //�^����_���[�W
        [Min(1)]
        public int _damage = 1;
    }

    [Serializable]
    public class StageEnemyDate
    {
        [Label("Stage�ԍ�")]
        public int _stageNum = 1;

        [Label("�^�O�w��"),Tag]
        public string _enemyTag;

        [Label("HP"),Min(1)]
        public int _enemyHp = 1;

        [Label("�U����")]
        public int _enemyPower = 1;

        [Label("�ړ��X�s�[�h"), Range(0.1f, 100f)] 
        public float _enemySpeed = 1;

        [Label("��_�{��")]
        public float _weekPointDamage=1;

        public float _attackSpan=1;
    }

    [SerializeField, Label("�_���[�W�������^�O�ꗗ"), Tag]public string[] _invalidTag;

    [Label("�I�u�W�F�N�g�_���[�W�ݒ�")]public SetDamageClass[] _setDamageClasses;

    [Label("�G�l�~�[�X�e�[�^�X�ݒ�")]public StageEnemyDate[] _stageEnemyDate;
}
