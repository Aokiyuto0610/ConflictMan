using System.Collections.Generic;
using UnityEngine;

public class ConfliObjMove : MonoBehaviour
{
    [SerializeField]
    private float minSpeed = 5f; // 最小スピード
    [SerializeField]
    private float maxSpeed = 20f; // 最大スピード
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

    public bool CanMove
    {
        get { return canMove; }
    }

    [SerializeField]
    private float maxDrag;

    private Queue<GameObject> arrowPool = new Queue<GameObject>();
    private List<GameObject> activeArrows = new List<GameObject>();
    private bool isDragging = false;

    [Header("Arrow Colors")]
    [SerializeField]
    private Color startColor = Color.red;
    [SerializeField]
    private Color middleColor = Color.yellow;
    [SerializeField]
    private Color endColor = Color.green;

    public bool isSelected { get; private set; }

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
                mouseEndPos = Input.mousePosition;
                Vector2 dragVector = mouseEndPos - mouseStartPos;
                float dragDistance = Mathf.Min(dragVector.magnitude / 100f, maxDrag);

                // スピードを計算
                float speed = Mathf.Lerp(minSpeed, maxSpeed, dragDistance / maxDrag);

                startDirection = -1 * dragVector.normalized;
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
            canMove = true;
        }
        else
        {
            ClearArrows();
            isDragging = false;
        }
    }

    void UpdateArrows(float dragDistance)
    {
        ClearArrows();

        int arrowCount = Mathf.FloorToInt((dragDistance / maxDrag) * maxArrows);
        arrowCount = Mathf.Clamp(arrowCount, 1, maxArrows);

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

                float t = arrowCount > 1 ? (float)i / (arrowCount - 1) : 0;
                Color gradientColor = t < 0.5f
                    ? Color.Lerp(startColor, middleColor, t * 2)
                    : Color.Lerp(middleColor, endColor, (t - 0.5f) * 2);

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
