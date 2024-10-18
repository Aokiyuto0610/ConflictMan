using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �V�[���J�ڎ��̃t�F�[�h�C���E�A�E�g�𐧌䂷�邽�߂̃N���X 
/// </summary>
public class FadeManager : MonoBehaviour
{

    #region Singleton

    private static FadeManager instance;

    public static FadeManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (FadeManager)FindObjectOfType(typeof(FadeManager));

                if (instance == null)
                {
                    Debug.LogError(typeof(FadeManager) + "is nothing");
                }
            }

            return instance;
        }
    }

    #endregion Singleton


    /// <summary>�t�F�[�h���̓����x</summary>
    private float fadeAlpha = 0;
    /// <summary>�t�F�[�h�����ǂ���</summary>
    private bool isFading = false;
    public bool IsFading => isFading;
    /// <summary>�t�F�[�h�F</summary>
    public Color fadeColor = Color.black;


    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void OnGUI()
    {

        // Fade 
        if (this.isFading)
        {
            this.fadeColor.a = this.fadeAlpha;
            GUI.color = this.fadeColor;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
        }

    }

    /// <summary>
    /// ��ʑJ�� 
    /// </summary>
    /// <param name='scene'>�V�[����</param>
    /// <param name='interval'>�Ó]�ɂ����鎞��(�b)</param>
    public bool LoadScene(string scene, float interval)
    {
        StartCoroutine(TransScene(scene, interval));
        return true;
    }

    /// <summary>
    /// �V�[���J�ڗp�R���[�`�� 
    /// </summary>
    /// <param name='scene'>�V�[����</param>
    /// <param name='interval'>�Ó]�ɂ����鎞��(�b)</param>
    private IEnumerator TransScene(string scene, float interval)
    {
     //���񂾂�Â� 
        this.isFading = true;
        float time = 0;
        while (time <= interval)
        {
            this.fadeAlpha = Mathf.Lerp(0f, 1f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }

        //�V�[���ؑ� 
        SceneManager.LoadScene(scene);

        //���񂾂񖾂邭
        time = 0;
        while (time <= interval)
        {
            this.fadeAlpha = Mathf.Lerp(1f, 0f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }

        this.isFading = false;
    }

    // FadeManager.Instance.LoadScene(�J�ڐ��scene��);
}

