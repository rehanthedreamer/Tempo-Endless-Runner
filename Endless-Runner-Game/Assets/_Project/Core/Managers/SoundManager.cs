using UnityEngine;
using System.Collections;

public class SoundManager : Singleton<SoundManager>
{
    // --------------------
    // SETTINGS
    // --------------------
    [SerializeField] private AudioSource bgMusicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private float fadeDuration = 1f;

    // --------------------
    // AUDIO CLIPS
    // --------------------
    public AudioClip bgMusic;
    public AudioClip btnClickSound;
    public AudioClip jumpSound;
    public AudioClip hitSound;
    public AudioClip runningSound;
    public AudioClip deadSound;
    public AudioClip coin;


    private Coroutine _fadeCoroutine;
    // --------------------
    // APPLY SETTINGS
    // --------------------

    public void ApplySettings()
    {
        bgMusicSource.mute = !SaveService.MusicOn;
        sfxSource.mute = !SaveService.SfxOn;
    }

    // --------------------
    // BG MUSIC
    // --------------------

    public void PlayBGMusic(AudioClip clip, float volume = 1f)
    {
        if (!SaveService.MusicOn)
            return;

        if (_fadeCoroutine != null)
            StopCoroutine(_fadeCoroutine);

        _fadeCoroutine = StartCoroutine(FadeInMusic(clip, volume));
    }

    public void StopBGMusic()
    {
        if (_fadeCoroutine != null)
            StopCoroutine(_fadeCoroutine);

        _fadeCoroutine = StartCoroutine(FadeOutMusic());
    }

    // --------------------
    // SFX
    // --------------------

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (!SaveService.SfxOn)
            return;

        sfxSource.PlayOneShot(clip, volume);
    }

    public void StopSFX()
    {
        if (!SaveService.SfxOn)
            return;

        sfxSource.Stop();

    }

    // --------------------
    // FADE
    // --------------------

    private IEnumerator FadeInMusic(AudioClip clip, float targetVolume)
    {
        if (bgMusicSource.isPlaying)
            yield return FadeOutMusic();

        bgMusicSource.clip = clip;
        bgMusicSource.volume = 0f;
        bgMusicSource.loop = true;
        bgMusicSource.Play();

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            bgMusicSource.volume = Mathf.Lerp(0f, targetVolume, t / fadeDuration);
            yield return null;
        }

        bgMusicSource.volume = targetVolume;
    }

    private IEnumerator FadeOutMusic()
    {
        float startVolume = bgMusicSource.volume;
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            bgMusicSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeDuration);
            yield return null;
        }

        bgMusicSource.Stop();
        bgMusicSource.volume = startVolume;
    }
}
