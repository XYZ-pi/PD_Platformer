using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1f; // на случай, если пришли из паузы
    }

    public void Play()
    {
        SceneManager.LoadScene(1); // первый уровень
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit"); // в редакторе игра не закрывается, это нормально
    }
}
