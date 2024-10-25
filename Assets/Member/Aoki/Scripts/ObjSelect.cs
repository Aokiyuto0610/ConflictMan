using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjSelect : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private ConfliObjMove move;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(Click());
        Debug.Log("aiu");
    }

    private IEnumerator Click()
    {
        yield return new WaitForSeconds(0.5f);
        move.goFlag = true;
    }
}
