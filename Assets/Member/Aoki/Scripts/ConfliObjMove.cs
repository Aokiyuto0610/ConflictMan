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
    private bool isDragging = false; // 引っ張り状態かどうかを管理

    [Header("Arrow Colors")]
    [SerializeField]
    private Color startColor = Color.red; // 先端
    [SerializeField]
    private Color middleColor = Color.yellow; // 中間
    [SerializeField]
    private Color endColor = Color.green; // 末端

    public bool isSelected { get; private set; } // 選択状態を管理

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
                // 引っ張り開始
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
                // 引っ張り終了
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
            canMove = true; // 再選択時に動かせるように
        }
        else
        {
            ClearArrows();
            isDragging = false; // 選択解除時に引っ張りをリセット
        }
    }

    void UpdateArrows(float dragDistance)
    {
        ClearArrows();

        int arrowCount = Mathf.FloorToInt((dragDistance / maxDrag) * maxArrows);
        arrowCount = Mathf.Clamp(arrowCount, 1, maxArrows);

        // カラーコードを指定
        string startColorCode = "#92D050"; // 開始色
        string middleColorCode = "#FF6600"; // 中間色
        string endColorCode = "#FF0000"; // 終了色

        Color startColor;
        Color middleColor;
        Color endColor;

        // カラーコードをColorに変換
        if (!ColorUtility.TryParseHtmlString(startColorCode, out startColor))
        {
            Debug.LogError($"Invalid color code: {startColorCode}");
            startColor = Color.red; // デフォルト色
        }

        if (!ColorUtility.TryParseHtmlString(middleColorCode, out middleColor))
        {
            Debug.LogError($"Invalid color code: {middleColorCode}");
            middleColor = Color.yellow; // デフォルト色
        }

        if (!ColorUtility.TryParseHtmlString(endColorCode, out endColor))
        {
            Debug.LogError($"Invalid color code: {endColorCode}");
            endColor = Color.green; // デフォルト色
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

                // 進行度に応じたカラーを計算
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

                // SpriteRendererにカラーを設定
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
