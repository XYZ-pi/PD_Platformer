using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer instance;

    [SerializeField] private AudioClip music;
    [SerializeField, Range(0f, 1f)] private float volume = 0.4f;
    [SerializeField, Range(0f, 1f)] private float duckFactor = 0.5f;   // 0.5 = на 50% тише
    [SerializeField, Range(0f, 1f)] private float uiVolume = 1f;       // громкость кликов

    private AudioSource musicSource;
    private AudioSource uiSource;

    private void Awake()
    {
        if (instance != null) { Destroy(gameObject); return; }
        instance = this;
        DontDestroyOnLoad(gameObject);

        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = music;
        musicSource.loop = true;
        musicSource.volume = volume;
        musicSource.Play();

        uiSource = gameObject.AddComponent<AudioSource>();
        uiSource.playOnAwake = false;
    }

    private void OnDestroy()
    {
        if (instance == this) instance = null;
    }

    // true: музыка тише (открыта панель), false: обычная громкость
    public static void SetDucked(bool ducked)
    {
        if (instance == null) return;
        instance.musicSource.volume = instance.volume * (ducked ? instance.duckFactor : 1f);
    }

    // клик по кнопке: играет через этот объект, потому что он не уничтожается
    // при смене сцены (иначе клик по Restart или Next level обрывался бы)
    public static void PlayUI(AudioClip clip)
    {
        if (clip == null) return;

        if (instance != null)
            instance.uiSource.PlayOneShot(clip, SoundFX.MasterVolume * instance.uiVolume);
        else
            SoundFX.Play(clip, "ui", 1f);   // если уровень запущен без главного меню
    }
}