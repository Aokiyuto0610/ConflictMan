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
        if (!move.goFlag)
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

            Debug.Log("ëIëÇ≥ÇÍÇΩÇÊ");
        }
        else
        {
            Debug.Log("îÚçsíÜÇÃÇΩÇﬂëIëÇ≈Ç´Ç‹ÇπÇÒ");
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
