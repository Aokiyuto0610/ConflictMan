using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Test : MonoBehaviour
{
    public string SceneName;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        { 
            // ç≤ñÏí«ãL
            FadeManager.Instance.LoadScene("Stage1", 1.0F);
            SoundManager.Instance.PlaySE(SESoundData.SE.Button);

        });

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
