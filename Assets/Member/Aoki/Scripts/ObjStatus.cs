using UnityEngine;

public class ObjStatus : MonoBehaviour
{
    [SerializeField]
    private int _initialBounce;
    public int _bounce;
    private bool _isSelected = false;
    private bool _isOnFloor = false;
    private bool _isGravity = false;
    private Rigidbody2D _rb;

    private float _floorContactTime = 0f;
    private const float ResetBounceThreshold = 0.3f;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _bounce = _initialBounce;
    }

    void Update()
    {
        if (_isOnFloor && !_isSelected)
        {
            _rb.gravityScale = 1;
            _floorContactTime += Time.deltaTime;

            if (_floorContactTime >= ResetBounceThreshold)
            {
                _bounce = _initialBounce;
                Debug.Log("Bounce count reset after staying on Floor for 0.5 seconds.");
            }
        }
        else
        {
            _floorContactTime = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Enemy"))
        {
            _bounce--;

            if (_bounce <= 0 && !_isGravity)
            {
                _isGravity = true;
                _rb.gravityScale = 1;
                _rb.velocity = Vector2.zero;
                Debug.Log("Object starts falling due to bounce limit reached.");
            }
        }

        if (other.gameObject.CompareTag("Floor"))
        {
            _isOnFloor = true;

            if (_bounce <= -1 && _isGravity)
            {
                _bounce = _initialBounce;
                _rb.velocity = Vector2.zero;
                _rb.gravityScale = 0;
                _isGravity = false;
                _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                DefaultRotation();
                Debug.Log("Object reset after hitting the floor.");
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            _isOnFloor = false;
            _floorContactTime = 0f; // —£‚ê‚½‚Æ‚«‚ÉÚGŽžŠÔ‚ðƒŠƒZƒbƒg
        }
    }

    public void DefaultRotation()
    {
        _rb.constraints = RigidbodyConstraints2D.None;
        Debug.Log("Object rotation reset.");
    }
}
