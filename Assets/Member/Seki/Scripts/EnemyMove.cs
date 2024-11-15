using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float _moveSpeed;

    private bool _isTurn;

    [SerializeField] private GameObject _parentObj;

    [SerializeField] private GameObject _renderObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isTurn)
        {
            _parentObj.transform.position = new Vector3(_parentObj.transform.position.x + (_moveSpeed / 100), _parentObj.transform.position.y, _parentObj .transform.position.z);
        }
        else
        {
            _parentObj.transform.position = new Vector3(_parentObj.transform.position.x - (_moveSpeed / 100), _parentObj.transform.position.y, _parentObj.transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            if (_isTurn)
            {
                _isTurn = false;
                _renderObj.transform.rotation = Quaternion.Euler(_parentObj.transform.rotation.x, 180, _parentObj.transform.rotation.z);
            }
            else if (!_isTurn)
            {
                _isTurn = true;
                _renderObj.transform.rotation = Quaternion.Euler(_parentObj.transform.rotation.x, 0, _parentObj.transform.rotation.z);
            }
            else
            {
                Debug.Log("ƒoƒO");
            }
        }
    }
}
