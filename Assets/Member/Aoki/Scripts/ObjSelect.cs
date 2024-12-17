using UnityEngine;
using UnityEngine.EventSystems;

public class ObjSelect : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private ConfliObjMove move;

    private static ObjSelect currentlySelected;
    private int clickCount = 0;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (currentlySelected == this)
        {
            clickCount++;

            if (clickCount >= 2)
            {
                // 2回目のクリックで引っ張り状態を有効化
                move.SetSelected(true);
                Debug.Log($"{gameObject.name} が引っ張り状態になりました");
                clickCount = 0; // クリックカウントをリセット
            }
        }
        else
        {
            if (currentlySelected != null)
            {
                currentlySelected.Deselect();
            }

            currentlySelected = this;
            Select();
            clickCount = 1; // 選択時に1回目のクリックとしてカウント
        }
    }

    private void Select()
    {
        move.SetSelected(true); // 1回目のクリックで選択状態に
        Debug.Log($"{gameObject.name} が選択されました");
    }

    private void Deselect()
    {
        move.SetSelected(false); // 選択解除時に
        Debug.Log($"{gameObject.name} の選択が解除されました");
    }
}
