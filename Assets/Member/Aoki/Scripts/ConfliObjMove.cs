using System.Collections;
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
    private float maxLine;
    [SerializeField]
    private LineRenderer lineRen;
    [SerializeField]
    private LayerMask wall;
    [SerializeField]
    private GameObject arrowPrefab;
    [SerializeField]
    private Transform arrowParent;
    [SerializeField]
    private float arrowSpacing;

    private List <GameObject> activeArrows = new List <GameObject>();


    public bool goFlag;
    // Start is called before the first frame update
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        goFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (goFlag == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mouseStartPos = Input.mousePosition;
            }
            if(Input.GetMouseButton(0))
            {
                mouseEndPos = Input.mousePosition;
                Vector2 dragVector = (mouseEndPos - mouseStartPos);
                startDirection = -dragVector.normalized;

                float dragDistance = Mathf.Clamp(dragVector.magnitude, 0, maxLine);
                UpdateArrows(dragDistance);
            }
            if (Input.GetMouseButtonUp(0))
            {
                mouseEndPos = Input.mousePosition;
                startDirection = -1 * (mouseEndPos - mouseStartPos).normalized;
                _rb2d.AddForce(startDirection * speed);
                ClearArrows();
            }
        }
    }

    private void UpdateArrows(float dragDistance)
    {
        ClearArrows();

        int arrowCount = Mathf.FloorToInt(dragDistance / arrowSpacing);
        for(int i = 1; i <= arrowCount; i++)
        {
            Vector2 arrowPosition = (Vector2)transform.position + startDirection * (i * arrowSpacing);
            GameObject arrow = Instantiate(arrowPrefab, arrowPosition, Quaternion.identity, arrowParent);
            arrow.transform.right = -startDirection; // –îˆó‚ÌŒü‚«‚ð’²®
            activeArrows.Add(arrow);
        }
    }

    void ClearArrows()
    {
        foreach (var arrow in activeArrows)
        {
            Destroy(arrow);
        }
        activeArrows.Clear();
    }
}
