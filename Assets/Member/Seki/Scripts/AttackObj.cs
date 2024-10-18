using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObj : MonoBehaviour
{
    [SerializeField, Label("弱点範囲に入っているか")] public bool _weakPoint=false;

    [SerializeField, Label("弱点範囲とするタグ"), Tag] string _weakPointTag;

    private bool _debugWealPoint;

    /*
    private void Start()
    {
        _debugWealPoint = _weakPoint;
    }

    private void Update()
    {
        //デバッグ用
        if (_debugWealPoint != _weakPoint)
        {
            Debug.Log(this.gameObject.name + " 弱点判定：" + _weakPoint);
        }
        _debugWealPoint = _weakPoint;
    }
    */

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == _weakPointTag)
        {
            //Debug.Log("弱点に入った");
            _weakPoint=true;
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == _weakPointTag)
        {
            //Debug.Log("弱点出た");
            _weakPoint=false;
        }
    }
}