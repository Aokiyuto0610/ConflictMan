using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{

    //秒
    private float sec;
    //分
    private float min;

    [SerializeField] Text dateTimeText;

    void Update()
    {
        //タイマースタート
        sec += Time.deltaTime;
        //秒が60秒より上いったら
        if (sec > 60)
        {
            //秒を０にする
            sec = 0;
            //分を１プラスする
            min++;
        }
    
        //タイマーをテキストに反映
        dateTimeText.text = min.ToString("00") + ":" + ((int)sec).ToString("00");
    }
}

