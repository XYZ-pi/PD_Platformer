using UnityEngine;
using UnityEngine.UI;

public class GameAudioSettings : MonoBehaviour
{
    [Header("Слайдер громкости")]
    [SerializeField] private Slider soundSlider;

    [Header("Тогглы")]
    [SerializeField] private Toggle muteAllToggle;
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle playerSfxToggle;
    [SerializeField] private Toggle collectibleToggle;

    private void Start()
    {
        if (soundSlider != null)
        {
            float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 0.5f);
            soundSlider.value = savedVolume;
            SoundFX.MasterVolume = savedVolume;
            soundSlider.onValueChanged.AddListener(SetVolume);
        }

        if (muteAllToggle != null) muteAllToggle.onValueChanged.AddListener(ToggleMuteAll);
        if (musicToggle != null) musicToggle.onValueChanged.AddListener(ToggleMusic);
        if (playerSfxToggle != null) playerSfxToggle.onValueChanged.AddListener(TogglePlayerSfx);
        if (collectibleToggle != null) collectibleToggle.onValueChanged.AddListener(ToggleCollectible);
    }

    private void OnEnable()
    {
        if (muteAllToggle != null)
            muteAllToggle.SetIsOnWithoutNotify(!AudioListener.pause);

        if (musicToggle != null)
        {
            MusicPlayer music = FindAnyObjectByType<MusicPlayer>();
            if (music != null)
            {
                AudioSource source = music.GetComponent<AudioSource>();
                if (source != null) musicToggle.SetIsOnWithoutNotify(source.enabled);
            }
        }

        if (playerSfxToggle != null)
            playerSfxToggle.SetIsOnWithoutNotify(SoundFX.IsPlayerSfxEnabled);

        if (collectibleToggle != null)
            collectibleToggle.SetIsOnWithoutNotify(SoundFX.IsCollectibleSfxEnabled);
    }

    public void SetVolume(float volume)
    {
        SoundFX.MasterVolume = volume;

        MusicPlayer music = FindAnyObjectByType<MusicPlayer>();
        if (music != null)
        {
            AudioSource source = music.GetComponent<AudioSource>();
            if (source != null) source.volume = 0.4f * volume;
        }

        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void ToggleMuteAll(bool isOn)
    {
        AudioListener.pause = !isOn;
    }

    public void ToggleMusic(bool isOn)
    {
        MusicPlayer music = FindAnyObjectByType<MusicPlayer>();
        if (music != null)
        {
            AudioSource source = music.GetComponent<AudioSource>();
            if (source != null) source.enabled = isOn;
        }
    }

    public void TogglePlayerSfx(bool isOn)
    {
        SoundFX.IsPlayerSfxEnabled = isOn;
    }

    public void ToggleCollectible(bool isOn)
    {
        SoundFX.IsCollectibleSfxEnabled = isOn;
    }
}