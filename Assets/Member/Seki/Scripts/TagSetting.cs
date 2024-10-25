using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class TagSetting : MonoBehaviour
{
    [SerializeField, Tag,Label("ê›íËÇ∑ÇÈÉ^ÉO")] string ThisObjTag;

    void Start()
    {
        this.gameObject.tag = ThisObjTag;
    }
}