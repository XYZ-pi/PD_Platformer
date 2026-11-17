using UnityEngine;
using UnityEngine.UI;

public class GameAudioSettings : MonoBehaviour
{
    [Header("Тогглы")]
    [SerializeField] private Toggle muteAllToggle;
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle playerSfxToggle;
    [SerializeField] private Toggle collectibleToggle;

    private void Start()
    {
        // Подписываемся на изменения каждого тоггла
        if (muteAllToggle != null) muteAllToggle.onValueChanged.AddListener(ToggleMuteAll);
        if (musicToggle != null) musicToggle.onValueChanged.AddListener(ToggleMusic);
        if (playerSfxToggle != null) playerSfxToggle.onValueChanged.AddListener(TogglePlayerSfx);
        if (collectibleToggle != null) collectibleToggle.onValueChanged.AddListener(ToggleCollectible);
    }

    public void ToggleMuteAll(bool isOn)
    {
        // Если isOn (галочка стоит) -> звук включен (пауза выключена)
        // Если крестик (isOn == false) -> выключаем весь звук полностью
        AudioListener.pause = !isOn;
    }

    public void ToggleMusic(bool isOn)
    {
        // Здесь можно выключать конкретный источник фоновой музыки (MusicPlayer)
        // Например, ищем объект с музыкальным плеером:
        MusicPlayer music = FindFirstObjectByType<MusicPlayer>();
        if (music != null)
        {
            // Предполагается, что у вашего MusicPlayer есть метод или доступ к AudioSource
            // Или просто выключаем компонент AudioSource на объекте музыки:
            AudioSource source = music.GetComponent<AudioSource>();
            if (source != null) source.enabled = isOn;
        }
    }

    public void TogglePlayerSfx(bool isOn)
    {
        // Логика для звуков игрока (например, через статический флаг или менеджер)
        Debug.Log("Звуки игрока: " + isOn);
    }

    public void ToggleCollectible(bool isOn)
    {
        // Логика для звуков предметов
        Debug.Log("Звуки предметов: " + isOn);
    }
}