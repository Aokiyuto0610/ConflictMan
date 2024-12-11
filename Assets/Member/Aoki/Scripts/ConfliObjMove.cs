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
    private LayerMask wall;
    [SerializeField]
    private GameObject arrowPrefab;
    [SerializeField]
    private Transform arrowParent;
    [SerializeField]
    private float arrowSpacing;

    [SerializeField]
    private LayerMask floorlay;
    private bool canMove = true;

    private List<GameObject> arrowPool = new List<GameObject>();
    private int activeArrowCount;

    public bool goFlag;

    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        goFlag = false;

        for (int i = 0; i < 8; i++)
        {
            GameObject arrow = Instantiate(arrowPrefab, arrowParent);
            arrow.SetActive(false);
            arrowPool.Add(arrow);
        }
    }

    void Update()
    {
        if (goFlag)
        {
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
                _rb2d.AddForce(startDirection * speed);
                ClearArrows();

                canMove = false;
            }
            else
            {
                // オブジェクトが静止していれば操作を再度有効化
                if (_rb2d.velocity.magnitude < 0.1f)
                {
                    canMove = true;
                }
            }
        }
    }

    void UpdateArrows(float dragDistance)
    {
        ClearArrows();

        int arrowCount = 0;
        if (dragDistance >= 5f)
        {
            arrowCount = 8;
        }
        else if (dragDistance >= 4f)
        {
            arrowCount = 4;
        }
        else if (dragDistance >= 3f)
        {
            arrowCount = 3;
        }
        else if (dragDistance >= 2f)
        {
            arrowCount = 2;
        }


        for (int i = 0; i < arrowCount; i++)
        {
            if (i < arrowPool.Count)
            {
                Vector2 arrowPosition = (Vector2)transform.position + startDirection * ((i + 1) * arrowSpacing);
                GameObject arrow = arrowPool[i];
                arrow.transform.position = arrowPosition;
                arrow.transform.right = -startDirection;
                arrow.SetActive(true);
            }
        }

        activeArrowCount = arrowCount;
    }

    void ClearArrows()
    {
        for (int i = 0; i < activeArrowCount; i++)
        {
            arrowPool[i].SetActive(false);
        }

        activeArrowCount = 0;
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
