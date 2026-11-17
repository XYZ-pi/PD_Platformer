    using UnityEngine;

    public static class SoundFX
    {
        public static float MasterVolume { get; set; } = 1f;

        // Флаги разрешений для разных типов звуков
        public static bool IsPlayerSfxEnabled { get; set; } = true;
        public static bool IsCollectibleSfxEnabled { get; set; } = true;

        // Метод для обычных звуков (теперь перегружен с типом звука)
        public static void Play(AudioClip clip, float volume = 1f)
        {
            Play(clip, "default", volume);
        }

        public static void Play(AudioClip clip, string soundType, float volume = 1f)
        {
            if (clip == null || Camera.main == null) return;

            // Проверяем включен ли конкретный тип звука
            if (soundType == "player" && !IsPlayerSfxEnabled) return;
            if (soundType == "collectible" && !IsCollectibleSfxEnabled) return;

            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, volume * MasterVolume);
        }
    }


