using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    private bool isTimerRunning = false;
    private float timeElapsed = 0f;

    public Text timerText;

    // シーン遷移後もタイマーオブジェクトを維持するために
    void Awake()
    {
        // このオブジェクトをシーン遷移後も保持する
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (isTimerRunning)
        {
            timeElapsed += Time.deltaTime;
        }

        // 時間を表示
        if (timerText != null)
        {
            timerText.text = "Time: " + timeElapsed.ToString("F2");
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        isTimerRunning = false;  // タイマーを再開
    }

    // シーン遷移時にイベントを解除
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // シーン遷移時にイベントを解除
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // タイマーの停止
    public void StopTimer()
    {
        isTimerRunning = false;
    }

    // タイマーのリセット
    public void ResetTimer()
    {
        timeElapsed = 0f;
    }

    // 時間を取得
    public float GetTimeElapsed()
    {
        return timeElapsed;
    }
}