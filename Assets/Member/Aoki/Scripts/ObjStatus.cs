using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjStatus : MonoBehaviour
{
    [SerializeField]
    private int _initialBounce; // �����o�E���X�񐔂��C���X�y�N�^�[�Őݒ�\
    public int _bounce;
    public bool moveflag;
    private bool _isGravity = false;
    Rigidbody2D _rb;
    [SerializeField]
    public GameObject _mii;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _bounce = _initialBounce; // ���������� _initialBounce �̒l���g�p
    }

    // Update is called once per frame
    void Update()
    {
        if (_bounce <= 0 && _isGravity == false)
        {
            _isGravity = true;
            _rb.GetComponent<Rigidbody2D>().gravityScale = 1;
            _rb.velocity = Vector2.zero;
            Debug.Log(_isGravity);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Floor"))
        {
            _bounce--;
        }
        if (other.gameObject.CompareTag("Floor"))
        {
            if (_bounce <= -1 && _isGravity == true)
            {
                _bounce = _initialBounce; // �����l�Ƀ��Z�b�g
                _rb.velocity = Vector2.zero;
                _rb.GetComponent<Rigidbody2D>().gravityScale = 0;
                _isGravity = false;
                _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                DefaultRotation();
            }
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            _bounce = 0;
        }
        if (other.gameObject.CompareTag("Conflict"))
        {
            if (moveflag == true)
            {

            }
            else
            {

            }
        }
    }

    public void DefaultRotation()
    {
        _rb.constraints = RigidbodyConstraints2D.None;
        Debug.Log("������...");
    }
}
