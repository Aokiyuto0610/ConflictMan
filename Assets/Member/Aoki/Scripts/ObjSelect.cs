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
                // 2��ڂ̃N���b�N�ň��������Ԃ�L����
                move.SetSelected(true);
                Debug.Log($"{gameObject.name} �����������ԂɂȂ�܂���");
                clickCount = 0; // �N���b�N�J�E���g�����Z�b�g
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
            clickCount = 1;
        }
    }

    private void Select()
    {
        move.SetSelected(true);
        Debug.Log($"{gameObject.name} ���I������܂���");
    }

    private void Deselect()
    {
        move.SetSelected(false);
        Debug.Log($"{gameObject.name} �̑I������������܂���");
    }
}
