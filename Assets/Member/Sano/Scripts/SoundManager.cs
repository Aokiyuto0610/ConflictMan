using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource bgmAudioSource;
    [SerializeField] AudioSource seAudioSource;

    [SerializeField] List<BGMSoundData> bgmSoundDatas;
    [SerializeField] List<SESoundData> seSoundDatas;

    public float masterVolume = 1;
    public float bgmMasterVolume = 1;
    public float seMasterVolume = 1;

    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

public void PlayBGM(BGMSoundData.BGM bgm)
{

    //�ؒǋL
    // �Đ�����BGM������Β�~
    if (bgmAudioSource.isPlaying)
    {
        bgmAudioSource.Stop();
    }

    // �V����BGM��ݒ肵�čĐ�
    BGMSoundData data = bgmSoundDatas.Find(data => data.bgm == bgm);
    if (data != null)
    {
        bgmAudioSource.clip = data.audioClip;
        bgmAudioSource.volume = data.volume * bgmMasterVolume * masterVolume;
        bgmAudioSource.Play();
    }
}

    public void PlaySE(SESoundData.SE se)
    {
        SESoundData data = seSoundDatas.Find(data => data.se == se);
        seAudioSource.volume = data.volume * seMasterVolume * masterVolume;
        seAudioSource.PlayOneShot(data.audioClip);
    }

}

[System.Serializable]
public class BGMSoundData
{
    public enum BGM // ���x��
    {
        Title,
        Stage1,
        Stage2,
        Stage3,
        Boss,
        Win,
        Lose
    }

    public BGM bgm;
    public AudioClip audioClip;
    [Range(0, 1)]
    public float volume = 1;
}

[System.Serializable]
public class SESoundData
{
    public enum SE // ���x�� 
    {
        Button,
        Damege,
        Shot,
        Action,
        Walltuch
    }

    public SE se;
    public AudioClip audioClip;
    [Range(0, 1)]
    public float volume = 1;
}