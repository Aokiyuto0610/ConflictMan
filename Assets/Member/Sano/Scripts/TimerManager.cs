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

    // �V�[���J�ڌ���^�C�}�[�I�u�W�F�N�g���ێ����邽�߂�
    void Awake()
    {
        // ���̃I�u�W�F�N�g���V�[���J�ڌ���ێ�����
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (isTimerRunning)
        {
            timeElapsed += Time.deltaTime;
        }

        // ���Ԃ�\��
        if (timerText != null)
        {
            timerText.text = "Time: " + timeElapsed.ToString("F2");
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        isTimerRunning = false;  // �^�C�}�[���ĊJ
    }

    // �V�[���J�ڎ��ɃC�x���g������
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // �V�[���J�ڎ��ɃC�x���g������
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // �^�C�}�[�̒�~
    public void StopTimer()
    {
        isTimerRunning = false;
    }

    // �^�C�}�[�̃��Z�b�g
    public void ResetTimer()
    {
        timeElapsed = 0f;
    }

    // ���Ԃ��擾
    public float GetTimeElapsed()
    {
        return timeElapsed;
    }
}