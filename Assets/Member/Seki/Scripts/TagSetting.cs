using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class TagSetting : MonoBehaviour
{
    [SerializeField, Tag,Label("�ݒ肷��^�O")] string ThisObjTag;

    void Start()
    {
        this.gameObject.tag = ThisObjTag;
    }
}