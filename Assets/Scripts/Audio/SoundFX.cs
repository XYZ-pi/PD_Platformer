using UnityEngine;

public static class SoundFX
{
    public static void Play(AudioClip clip, float volume = 0.1f)
    {
        if (clip == null || Camera.main == null) return;
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, volume);
    }
}


