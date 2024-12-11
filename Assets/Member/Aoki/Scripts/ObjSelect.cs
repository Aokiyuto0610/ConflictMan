using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjSelect : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private ConfliObjMove move;

    private static ObjSelect currentlySelected;

    private void Start()
    {
        if (currentlySelected == this)
        {
            currentlySelected = null;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!move.goFlag) // goFlagが有効な場合は選択できない
        {
            if (currentlySelected == this)
            {
                Deselect();
                currentlySelected = null;
            }
            else
            {
                if (currentlySelected != null)
                {
                    currentlySelected.Deselect();
                }

                currentlySelected = this;
                Select();
            }

            Debug.Log("選択されたよ");
        }
        else
        {
            Debug.Log("飛行中のため選択できません");
        }
    }

    private void Select()
    {
        StartCoroutine(Click());
    }

    private void Deselect()
    {
        move.goFlag = false;
    }

    private IEnumerator Click()
    {
        yield return new WaitForSeconds(0.5f);
        move.goFlag = true;
    }
}
