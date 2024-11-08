using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hpanim : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    public Animator _anim;
    // Start is called before the first frame update
    void Start()
    {
        _anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float dis = Vector2.Distance(this.transform.position, player.transform.position);
        Debug.Log("距離" + dis);

        if (dis < 4.0f)
        {
            //Bool型のパラメーターをTrueにする
            _anim.SetBool("HpBool", true);
        }
        else
        {
            _anim.SetBool("HpBool", false);
        }
    }
}
