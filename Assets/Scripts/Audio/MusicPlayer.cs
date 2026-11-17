using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer instance;

    [SerializeField] private AudioClip music;
    [SerializeField, Range(0f, 1f)] private float volume = 0.4f;

    private void Awake()
    {
        if (instance != null) { Destroy(gameObject); return; }
        instance = this;
        DontDestroyOnLoad(gameObject);

        var src = gameObject.AddComponent<AudioSource>();
        src.clip = music;
        src.loop = true;
        src.volume = volume;
        src.Play();
    }
}

