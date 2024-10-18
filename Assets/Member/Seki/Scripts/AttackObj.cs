using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObj : MonoBehaviour
{
    [SerializeField, Label("��_�͈͂ɓ����Ă��邩")] public bool _weakPoint=false;

    [SerializeField, Label("��_�͈͂Ƃ���^�O"), Tag] string _weakPointTag;

    private bool _debugWealPoint;

    /*
    private void Start()
    {
        _debugWealPoint = _weakPoint;
    }

    private void Update()
    {
        //�f�o�b�O�p
        if (_debugWealPoint != _weakPoint)
        {
            Debug.Log(this.gameObject.name + " ��_����F" + _weakPoint);
        }
        _debugWealPoint = _weakPoint;
    }
    */

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == _weakPointTag)
        {
            //Debug.Log("��_�ɓ�����");
            _weakPoint=true;
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == _weakPointTag)
        {
            //Debug.Log("��_�o��");
            _weakPoint=false;
        }
    }
}