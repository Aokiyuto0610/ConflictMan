using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;

public class EnemyState : MonoBehaviour
{
    //�_���[�W���
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

    //�G���
    [Serializable]
    public class StageEnemyDate
    {
        [Label("Stage�ԍ�")]
        public int _stageNum = 1;

        [Label("���̃X�e�[�W�ŉ��̖ڂ�")] 
        public int _enemyNum;

        [Label("�^�O�w��"),Tag]
        public string _enemyTag;

        [Label("HP"),Min(1)]
        public int _enemyHp = 1;

        [Label("�U����")]
        public int _enemyPower = 1;

        [Label("�ړ��X�s�[�h"), Range(0.1f, 99f)] 
        public float _enemySpeed = 1;

        [Label("��_�{��")]
        public float _weekPointDamage=1;

        [Label("�U�����o")]
        float[] _enemyAttackSpan;

        [Label("�U�����")]
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


    [Label("�G�̍U����������Ȃ��I�u�W�F�N�g�^�O"), Tag] public string[] _notEnemyAttackTag;

    [Label("�G�ւ̃_���[�W�𖳌�������v���C���[�̃^�O"), Tag]public string[] _notPlayerAttackTag;

    [Label("�I�u�W�F�N�g�_���[�W�ݒ�")]public SetDamageClass[] _setDamageClasses;

    [Label("�G�l�~�[�X�e�[�^�X�ݒ�")]public StageEnemyDate[] _stageEnemyDate;

    [Label("���˂����񐔂ɉ������ω����l")] public int _reflectionMagnification = 20;

    [Label("�܂�Ԃ��n�_�̃^�O"), Tag] public string _turnPointTag;

    [Label("���̃Q�[����ɑ��݂���G��")] public int _enemyTotal;

    [Label("�|���ꂽ�G��")] public int _enemySlainNum = 0;

    [Label("���U���gScene"), Scene] public string _resultScene;

    [Label("�X�^�[�g��")] public bool _gameStart = false;

    private void Start()
    {
        Debug.Log(_stageEnemyDate.Length);
        _enemyTotal=_stageEnemyDate.Length;
    }

    //�G���|���ꂽ�ۂɌĂяo��
    public void EnemySlain()
    {
        //����������
        _enemySlainNum++;

        //�S�ē����ł�����
        if (_enemySlainNum >= _enemyTotal)
        {
            //���U���g�ɑJ��
            FadeManager.Instance.LoadScene(_resultScene, 1f);
        }
    }
}
