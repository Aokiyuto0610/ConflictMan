using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultButton : MonoBehaviour
{
    [SerializeField, Scene] string NextScene;

    public void OnClick()
    {
        FadeManager.Instance.LoadScene(NextScene, 1f);
    }
}
