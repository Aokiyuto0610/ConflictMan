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

    private Queue<GameObject> arrowPool = new Queue<GameObject>();
    private List<GameObject> activeArrows = new List<GameObject>();
    public bool goFlag;

    //[SerializeField] 
    //private Material arrowMaterial; // シェーダー適用済みマテリアル
    //[SerializeField] 
    //private Texture2D gradientTexture; // グラデーション用テクスチャ


    void Start()
    {

        //if (arrowMaterial != null && gradientTexture != null)
        //{
        //    arrowMaterial.SetTexture("_GradientTex", gradientTexture);
        //}

        _rb2d = GetComponent<Rigidbody2D>();
        goFlag = false;

        for (int i = 0; i < maxArrows; i++)
        {
            GameObject arrow = Instantiate(arrowPrefab, arrowParent);
            arrow.SetActive(false);
            arrowPool.Enqueue(arrow);
        }
    }

    void Update()
    {
        if (!goFlag) return;

        if (Input.GetMouseButtonDown(0))
        {
            mouseStartPos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            mouseEndPos = Input.mousePosition;
            Vector2 dragVector = mouseEndPos - mouseStartPos;
            startDirection = -dragVector.normalized;

            float dragDistance = dragVector.magnitude / 100f;
            UpdateArrows(dragDistance);
        }

        if (Input.GetMouseButtonUp(0))
        {
            mouseEndPos = Input.mousePosition;
            startDirection = -1 * (mouseEndPos - mouseStartPos).normalized;
            _rb2d.AddForce(startDirection * speed, ForceMode2D.Impulse);
            ClearArrows();

            canMove = false;
        }

        if (_rb2d.velocity.magnitude < 0.1f)
        {
            canMove = true;
        }
    }

    void UpdateArrows(float dragDistance)
    {
        ClearArrows();

        int arrowCount = Mathf.Clamp(Mathf.FloorToInt(dragDistance), 1, maxArrows);
        string startColorCode = "#FFF2CC";
        string endColorCode = "#FF0000";

        Color startColor;
        Color endColor;

        if (!ColorUtility.TryParseHtmlString(startColorCode, out startColor))
        {
            Debug.LogError($"Invalid color code: {startColorCode}");
            startColor = Color.red;
        }

        if (!ColorUtility.TryParseHtmlString(endColorCode, out endColor))
        {
            Debug.LogError($"Invalid color code: {endColorCode}");
            endColor = Color.green;
        }

        for (int i = 0; i < arrowCount; i++)
        {
            GameObject arrow = GetArrowFromPool();
            if (arrow != null)
            {
                Vector2 arrowPosition = (Vector2)transform.position + new Vector2(0, 0) + startDirection * ((i + 1) * arrowSpacing);
                arrow.transform.position = arrowPosition;

                float angle = Mathf.Atan2(startDirection.y, startDirection.x) * Mathf.Rad2Deg;
                arrow.transform.rotation = Quaternion.Euler(0, 0, angle - 90);

                arrow.SetActive(true);
                activeArrows.Add(arrow);

                float t = arrowCount > 1 ? (float)i / (arrowCount - 1) : 0;
                Color gradientColor = Color.Lerp(startColor, endColor, t);

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & floorlay) != 0)
        {
            Debug.Log("Floorに接触したよ");
            canMove = true;
        }
    }
}
