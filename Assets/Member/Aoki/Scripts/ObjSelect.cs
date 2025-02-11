using UnityEngine;
using UnityEngine.EventSystems;

public class ObjSelect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private ConfliObjMove move;

    private static ObjSelect currentlySelected;
    private bool isHolding = false;
    private float holdTime = 0f;
    private float requiredHoldDuration = 0.001f;

    private void Update()
    {
        if (isHolding)
        {
            holdTime += Time.deltaTime;

            if (holdTime >= requiredHoldDuration)
            {
                move.SetSelected(true);
                Debug.Log($"{gameObject.name} が引っ張り状態になりました");
                isHolding = false;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (currentlySelected == this)
        {
            isHolding = true;
            holdTime = 0f;
        }
        else
        {
            if (currentlySelected != null)
            {
                currentlySelected.Deselect();
            }

            currentlySelected = this;
            Select();
            isHolding = true;
            holdTime = 0f;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHolding = false;

        if (holdTime < requiredHoldDuration)
        {
            move.SetSelected(false);
            Debug.Log($"{gameObject.name} の選択が解除されました");
        }
    }

    private void Select()
    {
        move.SetSelected(true);
        Debug.Log($"{gameObject.name} が選択されました");
    }

    private void Deselect()
    {
        move.SetSelected(false);
        Debug.Log($"{gameObject.name} の選択が解除されました");
    }
}
