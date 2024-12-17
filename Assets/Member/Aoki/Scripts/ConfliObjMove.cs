using System.Collections.Generic;
using UnityEngine;

public class ConfliObjMove : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Rigidbody2D _rb2d;
    private Vector2 mouseStartPos;
    private Vector2 mouseEndPos;
    private Vector2 startDirection;

    [SerializeField]
    private GameObject arrowPrefab;
    [SerializeField]
    private Transform arrowParent;
    [SerializeField]
    private float arrowSpacing;

    [SerializeField]
    private int maxArrows;
    [SerializeField]
    private LayerMask floorlay;
    private bool canMove = true;

    [SerializeField]
    private float maxDrag;

    private Queue<GameObject> arrowPool = new Queue<GameObject>();
    private List<GameObject> activeArrows = new List<GameObject>();
    private bool isDragging = false; // ���������Ԃ��ǂ������Ǘ�

    [Header("Arrow Colors")]
    [SerializeField]
    private Color startColor = Color.red; // ��[
    [SerializeField]
    private Color middleColor = Color.yellow; // ����
    [SerializeField]
    private Color endColor = Color.green; // ���[

    public bool isSelected { get; private set; } // �I����Ԃ��Ǘ�

    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();

        for (int i = 0; i < maxArrows; i++)
        {
            GameObject arrow = Instantiate(arrowPrefab, arrowParent);
            arrow.SetActive(false);
            arrowPool.Enqueue(arrow);
        }
    }

    void Update()
    {
        if (!isSelected || !canMove) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (!isDragging)
            {
                // ��������J�n
                isDragging = true;
                mouseStartPos = Input.mousePosition;
            }
        }

        if (isDragging)
        {
            if (Input.GetMouseButton(0))
            {
                mouseEndPos = Input.mousePosition;
                Vector2 dragVector = mouseEndPos - mouseStartPos;
                startDirection = -dragVector.normalized;

                float dragDistance = Mathf.Min(dragVector.magnitude / 100f, maxDrag);
                UpdateArrows(dragDistance);
            }

            if (Input.GetMouseButtonUp(0))
            {
                // ��������I��
                mouseEndPos = Input.mousePosition;
                startDirection = -1 * (mouseEndPos - mouseStartPos).normalized;
                _rb2d.AddForce(startDirection * speed, ForceMode2D.Impulse);
                ClearArrows();

                isDragging = false;
                canMove = false;
            }
        }

        if (_rb2d.velocity.magnitude < 0.1f && !canMove)
        {
            canMove = true;
        }
    }

    public void SetSelected(bool selected)
    {
        isSelected = selected;

        if (selected)
        {
            canMove = true; // �đI�����ɓ�������悤��
        }
        else
        {
            ClearArrows();
            isDragging = false; // �I���������Ɉ�����������Z�b�g
        }
    }

    void UpdateArrows(float dragDistance)
    {
        ClearArrows();

        int arrowCount = Mathf.FloorToInt((dragDistance / maxDrag) * maxArrows);
        arrowCount = Mathf.Clamp(arrowCount, 1, maxArrows);

        // �J���[�R�[�h���w��
        string startColorCode = "#92D050"; // �J�n�F
        string middleColorCode = "#FF6600"; // ���ԐF
        string endColorCode = "#FF0000"; // �I���F

        Color startColor;
        Color middleColor;
        Color endColor;

        // �J���[�R�[�h��Color�ɕϊ�
        if (!ColorUtility.TryParseHtmlString(startColorCode, out startColor))
        {
            Debug.LogError($"Invalid color code: {startColorCode}");
            startColor = Color.red; // �f�t�H���g�F
        }

        if (!ColorUtility.TryParseHtmlString(middleColorCode, out middleColor))
        {
            Debug.LogError($"Invalid color code: {middleColorCode}");
            middleColor = Color.yellow; // �f�t�H���g�F
        }

        if (!ColorUtility.TryParseHtmlString(endColorCode, out endColor))
        {
            Debug.LogError($"Invalid color code: {endColorCode}");
            endColor = Color.green; // �f�t�H���g�F
        }

        for (int i = 0; i < arrowCount; i++)
        {
            GameObject arrow = GetArrowFromPool();
            if (arrow != null)
            {
                Vector2 arrowPosition = (Vector2)transform.position + startDirection * ((i + 1) * arrowSpacing);
                arrow.transform.position = arrowPosition;

                float angle = Mathf.Atan2(startDirection.y, startDirection.x) * Mathf.Rad2Deg;
                arrow.transform.rotation = Quaternion.Euler(0, 0, angle - 90);

                arrow.SetActive(true);
                activeArrows.Add(arrow);

                // �i�s�x�ɉ������J���[���v�Z
                float t = arrowCount > 1 ? (float)i / (arrowCount - 1) : 0;

                Color gradientColor;
                if (t < 0.5f)
                {
                    gradientColor = Color.Lerp(startColor, middleColor, t * 2);
                }
                else
                {
                    gradientColor = Color.Lerp(middleColor, endColor, (t - 0.5f) * 2);
                }

                // SpriteRenderer�ɃJ���[��ݒ�
                SpriteRenderer sr = arrow.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    sr.color = gradientColor;
                }
            }
        }
    }

    void ClearArrows()
    {
        foreach (var arrow in activeArrows)
        {
            arrow.SetActive(false);
            arrowPool.Enqueue(arrow);
        }
        activeArrows.Clear();
    }

    GameObject GetArrowFromPool()
    {
        if (arrowPool.Count > 0)
        {
            return arrowPool.Dequeue();
        }
        else
        {
            Debug.LogWarning("Arrow pool is empty!");
            return null;
        }
    }
}
