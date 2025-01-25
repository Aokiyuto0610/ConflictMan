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
        // シーン名または独自の条件に応じてBGMを再生
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
                Debug.LogWarning($"シーン {scene.name} に対応するBGMが設定されていません");
                break;
        }
    }
}
