using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    private TextMeshProUGUI bounceText;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _bounce = _initialBounce;
        UpdateBounceText();
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
                UpdateBounceText();
                Debug.Log("<b><i>Bounce count reset after staying on Floor for 0.5 seconds.</i></b>");
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
            UpdateBounceText();

            if (_bounce <= 0 && !_isGravity)
            {
                _isGravity = true;
                _rb.gravityScale = 1;
                _rb.velocity = Vector2.zero;
                Debug.Log("<b><i>Object starts falling due to bounce limit reached.</i></b>");
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
                UpdateBounceText();
                Debug.Log("<b><i>Object reset after hitting the floor.</i></b>");
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            _isOnFloor = false;
            _floorContactTime = 0f; // ó£ÇÍÇΩÇ∆Ç´Ç…ê⁄êGéûä‘ÇÉäÉZÉbÉg
        }
    }

    private void UpdateBounceText()
    {
        if (bounceText != null)
        {
            bounceText.text = "<b><i>: " + _bounce + "</i></b>";
        }
    }

    public void DefaultRotation()
    {
        _rb.constraints = RigidbodyConstraints2D.None;
        Debug.Log("<b><i>Object rotation reset.</i></b>");
    }
}
