using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("AudioManager is null.");
            }
            return _instance;
        }
    }

    public AudioClip bgmTitle, bgmStage, battleBgm, winBgm, defeatBgm;
    public AudioClip seExplosion;

    [SerializeField]
    private AudioSource bgmAudioSource;
    [SerializeField]
    private AudioSource seAudioSource;
    void Awake()
    {
        // もし_audioManagerが存在しなければ自己生成し、シングルトーンオブジェクトにする
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }

        // 最初はタイトルBGMを再生する
        PlayBGM(bgmTitle);
        //audioMixer.SetFloat("Master", 0.7f);
    }

    // BGMを再生する
    public void PlayBGM(AudioClip clip)
    {
        // 既に同じBGMが再生されている場合は何もしない
        if (bgmAudioSource.clip == clip && bgmAudioSource.isPlaying)
        {
            return;
        }

        // BGMを変更する
        bgmAudioSource.clip = clip;

        // BGMを再生する
        bgmAudioSource.Play();
    }

    // SEを再生する
    public void PlaySE(AudioClip clip)
    {
        seAudioSource.clip = clip;
        seAudioSource.PlayOneShot(clip);
    }

    public void SetBGMVolume(float volume)
    {
        bgmAudioSource.volume = volume;
    }

    public void SetSEVolume(float volume)
    {
        seAudioSource.volume = volume;
    }

    public void StopBGM()
    {
        bgmAudioSource.Stop();
    }

    public void StopSE()
    {
        seAudioSource.Stop();
    }
    public void SetLoop(bool isLoop)
    {
        bgmAudioSource.loop = isLoop;
    }
}
