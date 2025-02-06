using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangr : MonoBehaviour
{
    // クラスの取得
    [SerializeField]
    public Fade _fade;

    // どこからでも一文で呼べるようにする。 
    public static SceneChangr scenechangrInstance;

    private void Awake()
    {
        // 同じ処理を2回しないようにする
        if (scenechangrInstance == null)
        {
            // 変数の中にスクリプトを格納
            scenechangrInstance = GetComponent<SceneChangr>();
        }
    }
}

// public staticがメイン
// staticはどこからでも持ってこれる
