using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBGMManager : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // �V�[�����܂��͓Ǝ��̏����ɉ�����BGM���Đ�
        switch (scene.name)
        {
            case "Title":
                SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Title);
                break;
            case "Stage1":
                SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Boss);
                break;
            case "WinResult":
                SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Win);
                break;
            case "LoseResult":
                SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Lose);
                break;
            default:
                Debug.LogWarning($"�V�[�� {scene.name} �ɑΉ�����BGM���ݒ肳��Ă��܂���");
                break;
        }
    }
}
