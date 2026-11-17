using UnityEngine;
using UnityEngine.UI;

public class AutoButtonSounds : MonoBehaviour
{
    [SerializeField] private AudioClip clickClip;

    private void Awake()
    {
        // находит все кнопки, в том числе в выключенных панелях (победа, пауза, поражение)
        foreach (var b in GetComponentsInChildren<Button>(true))
            b.onClick.AddListener(() => MusicPlayer.PlayUI(clickClip));
    }
}