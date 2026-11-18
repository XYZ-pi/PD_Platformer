using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour

{
    [SerializeField] private GameObject mainMenuPanel;      
    [SerializeField] private GameObject settingsMenuPanel;
    [SerializeField] private GameObject audioMenuPanel;
    [SerializeField] private GameObject controlsMenuPanel;

    private void Start()
    {
        Time.timeScale = 1f; 
        OpenMainMenu();
    }

    public void Play()
    {
        SceneManager.LoadScene(1); 
    }

    public void OpenSettings()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (settingsMenuPanel != null) settingsMenuPanel.SetActive(true);
    }

    public void OpenMainMenu()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
        if (settingsMenuPanel != null) settingsMenuPanel.SetActive(false);
    }
    public void OpenAudioMenu()
    {
        if (settingsMenuPanel != null) settingsMenuPanel.SetActive(false);
        if (audioMenuPanel != null) audioMenuPanel.SetActive(true);
    }

    public void CloseAudioMenu()
    {
        if (audioMenuPanel != null) audioMenuPanel.SetActive(false);     // Выключаем панель аудио
        if (settingsMenuPanel != null) settingsMenuPanel.SetActive(true); // Включаем настройки
    }

    public void OpenControlsMenu()
    {
        if (settingsMenuPanel != null) settingsMenuPanel.SetActive(false);
        if (controlsMenuPanel != null) controlsMenuPanel.SetActive(true);
    }

    public void CloseControlsMenu()
    {
        if (controlsMenuPanel != null) controlsMenuPanel.SetActive(false);     // Выключаем панель аудио
        if (settingsMenuPanel != null) settingsMenuPanel.SetActive(true); // Включаем настройки
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit"); 
    }
}
