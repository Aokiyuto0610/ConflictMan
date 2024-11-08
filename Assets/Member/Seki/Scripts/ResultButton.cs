using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultButton : MonoBehaviour
{
    [SerializeField, Scene] string ResultScene;

    public void OnClick()
    {
        FadeManager.Instance.LoadScene(ResultScene, 1f);
    }
}
