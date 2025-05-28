using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    [Header("BGM Clips")]
    public AudioClip mainMenuClip;
    public AudioClip stageSelectClip;
    public AudioClip loadingClip;
    public AudioClip stage0Clip;
    public AudioClip stage1Clip;
    public AudioClip stage2Clip;

    [Header("SFX Clips")]
    public AudioClip coinClip;
    public AudioClip specialCoinClip;

    [Header("Settings")]
    public float fadeDuration = 1.5f;

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
            return;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // ✅ 씬 로딩 후 자동으로 BGM 변경
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("현재 로드된 씬: " + scene.name);

        switch (scene.name)
        {
            case "TitleScene":
                ChangeBGM(mainMenuClip);
                break;
            case "StageSelectScene":
                ChangeBGM(stageSelectClip);
                break;
            case "LoadingScene":
                ChangeBGM(loadingClip);
                break;
            case "Stage0":
                ChangeBGM(stage0Clip);
                break;
            case "Stage1":
                ChangeBGM(stage1Clip);
                break;
            case "Stage2":
                ChangeBGM(stage2Clip);
                break;
            case "TestStage":
                ChangeBGM(stage1Clip);
                break;
            default:
                Debug.LogWarning("해당 씬에 BGM 설정 없음: " + scene.name);
                break;
        }
    }

    public void ChangeBGM(AudioClip newClip)
    {
        if (bgmSource.clip == newClip || newClip == null) return;
        StopAllCoroutines();
        StartCoroutine(FadeOutIn(newClip));
    }

    private IEnumerator FadeOutIn(AudioClip newClip)
    {
        float startVolume = bgmSource.volume;

        // Fade Out
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            bgmSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeDuration);
            yield return null;
        }

        bgmSource.Stop();
        bgmSource.clip = newClip;
        bgmSource.Play();

        // Fade In
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            bgmSource.volume = Mathf.Lerp(0f, startVolume, t / fadeDuration);
            yield return null;
        }

        bgmSource.volume = startVolume;
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
            sfxSource.PlayOneShot(clip);
    }

    public void PlayCoinSFX()
    {
        PlaySFX(coinClip);
    }

    public void PlaySpecialCoinSFX()
    {
        PlaySFX(specialCoinClip);
    }

    // 필요 시 사용
    public void SetSFXVolume(float value)
    {
        if (sfxSource != null)
            sfxSource.volume = value;
    }

    public void SetBGMVolume(float value)
    {
        if (bgmSource != null)
            bgmSource.volume = value;
    }
}
