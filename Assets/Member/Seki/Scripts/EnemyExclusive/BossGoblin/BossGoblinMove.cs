using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;


public class BossGoblinMove : MonoBehaviour
{
    public float _moveSpeed;

    private bool _isTurn;

    [SerializeField] private GameObject _parentObj;

    [SerializeField] private GameObject _renderObj;

    [SerializeField] private GameObject _hpBarObj;

    [SerializeField] BossGoblinAttack _attackSc;

    [SerializeField] private GameObject _bodySp;

    [SerializeField] public Quaternion _defaultrot;

    [SerializeField]private bool _isRot=true;


    // Start is called before the first frame update
    void Start()
    {
        _defaultrot = _bodySp.transform.localRotation;
        Firstrot();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_attackSc._attacking)
        {
            if (_isRot)
            {
                _isRot = false;
                Firstrot();
            }
            if (_isTurn)
            {
                _parentObj.transform.position = new Vector3(_parentObj.transform.position.x + (_moveSpeed / 100), _parentObj.transform.position.y, _parentObj.transform.position.z);
            }
            else
            {
                _parentObj.transform.position = new Vector3(_parentObj.transform.position.x - (_moveSpeed / 100), _parentObj.transform.position.y, _parentObj.transform.position.z);
            }
        }
        else
        {
            _isRot=true;
            _bodySp.transform.localRotation= _defaultrot;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            if (_isTurn)
            {
                _isTurn = false;
                if (_attackSc._attackRight)
                {
                    _attackSc._attackRight = false;
                }
                else
                {
                    _attackSc._attackRight = true;
                }
                _renderObj.transform.rotation = Quaternion.Euler(_parentObj.transform.rotation.x, 180, _parentObj.transform.rotation.z);
                _hpBarObj.transform.rotation = Quaternion.Euler(_hpBarObj.transform.rotation.x, 180, _hpBarObj.transform.rotation.z);
            }
            else if (!_isTurn)
            {
                _isTurn = true;
                if (_attackSc._attackRight)
                {
                    _attackSc._attackRight = false;
                }
                else
                {
                    _attackSc._attackRight = true;
                }
                _renderObj.transform.rotation = Quaternion.Euler(_parentObj.transform.rotation.x, 0, _parentObj.transform.rotation.z);
                _hpBarObj.transform.rotation = Quaternion.Euler(_hpBarObj.transform.rotation.x, 180, _hpBarObj.transform.rotation.z);
            }
            else
            {
                Debug.Log("ƒoƒO");
            }
        }
    }

    private async void Firstrot()
    {
        await _bodySp.transform.DOLocalRotate(new Vector3(0, 0, -10), 0.5f, RotateMode.LocalAxisAdd).AsyncWaitForCompletion();
        if (!_attackSc._attacking)
        {
            Purasurot();
        }
    }

    private async void mainasurot()
    {
        await _bodySp.transform.DOLocalRotate(new Vector3(0, 0, -20), 0.5f, RotateMode.LocalAxisAdd).AsyncWaitForCompletion();
        if (!_attackSc._attacking)
        {
            Purasurot();
        }
    }

    private async void Purasurot()
    { 
        await _bodySp.transform.DOLocalRotate(new Vector3(0, 0, 20), 0.5f, RotateMode.LocalAxisAdd).AsyncWaitForCompletion();
        if (!_attackSc._attacking)
        {
            mainasurot();
        }
    }

    public void MoveReset()
    {
        _bodySp.transform.localRotation = _defaultrot;
    }
}
