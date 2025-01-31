using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowMove : MonoBehaviour
{

    //âeåπ
    [SerializeField]Transform _transform;


    void Update()
    {
        this.transform.position = _transform.transform.position-new Vector3(-0.1f,0.1f,0);
    }
}
