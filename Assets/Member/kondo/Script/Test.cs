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
<<<<<<< HEAD
        //SoundManager.Instance.PlaySE(SESoundData.SE.Button);
=======
        GetComponent<Button>().onClick.AddListener(() =>
        { 
            // ²–ì’Ç‹L
            FadeManager.Instance.LoadScene("Stage1", 1.0F);
            SoundManager.Instance.PlaySE(SESoundData.SE.Button);

        });

>>>>>>> origin/Sano
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
