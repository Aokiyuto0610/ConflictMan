using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using System;


public class Test : MonoBehaviour
{
    public FadeManager manager;
    public string SceneName;
    void Start()
    {
        ////SoundManager.Instance.PlaySE(SESoundData.SE.Button);
        //GetComponent<Button>().onClick.AddListener(() =>
        //{
        //    // ç≤ñÏí«ãL
        //    FadeManager.Instance.LoadScene("Stage1", 1.0F);
        //    SoundManager.Instance.PlaySE(SESoundData.SE.Button);

        //});

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnStart_button()
    {
        FadeManager.Instance.LoadScene("Stage1", 1.0F);
        SoundManager.Instance.PlaySE(SESoundData.SE.Button);
        Debug.Log("aa");
    }

    public void OnTitleBackButton()
    {
        FadeManager.Instance.LoadScene("Title", 1.0F);
        SoundManager.Instance.PlaySE(SESoundData.SE.Button);
        Debug.Log("aiu");
    }
}
