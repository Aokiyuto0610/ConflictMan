using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class TagSetting : MonoBehaviour
{
    [SerializeField, Tag,Label("設定するタグ")] string ThisObjTag;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.tag = ThisObjTag;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
