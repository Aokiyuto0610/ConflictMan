using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using System;


public class Test : MonoBehaviour
{
    public FadeManager manager;
    public string SceneName;
    // Start is called before the first frame update
    void Start()
    {
        //SoundManager.Instance.PlaySE(SESoundData.SE.Button);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Start_button()
    {
        //SceneChangr.scenechangrInstance._fade.SceneFade("Stage1");
        SoundManager.Instance.PlaySE(SESoundData.SE.Button);
    }
}
