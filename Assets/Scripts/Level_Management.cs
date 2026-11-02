using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Level_Management:MonoBehaviour
{
    public static Level_Management Instance { get; private set; }

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI fruitText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private TextMeshProUGUI resultTimeText;
    [SerializeField] private TextMeshProUGUI ratingText;

    [Header("Оценка по времени (секунды)")]
    [SerializeField] private float greenTime = 30f;   // быстрее этого: хорошо
    [SerializeField] private float yellowTime = 60f;  // быстрее этого: не очень, дольше: очень долго

    private int collected;
    private int total;
    private float timer;
    private bool finished;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pausePanel;
    private bool paused;

    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;

        // Считаем все фрукты на сцене автоматически
        total = 0;
        foreach (var c in FindObjectsByType<Fruits_Collectible>())
            total += c.Value;

        UpdateFruitUI();

        if (winPanel != null)
            winPanel.SetActive(false);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

    private void Update()
    {
        if (finished) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused) Resume();
            else Pause();
        }

        timer += Time.deltaTime;
        if (timerText != null)
            timerText.text = FormatTime(timer);

        //if (Input.GetKeyDown(KeyCode.G)) GameOver();
    }

    public void AddFruit(int value)
    {
        if (finished) return;

        collected += value;
        UpdateFruitUI();

        if (collected >= total)
            Win();
    }

    private void UpdateFruitUI()
    {
        if (fruitText != null)
            fruitText.text = collected + "/" + total;
    }

    private void Win()
    {
        finished = true;

        if (resultTimeText != null)
            resultTimeText.text = "Время: " + FormatTime(timer);

        if (ratingText != null)
        {
            if (timer <= greenTime)
            {
                ratingText.text = "Отлично!";
                ratingText.color = Color.green;
            }
            else if (timer <= yellowTime)
            {
                ratingText.text = "Неплохо";
                ratingText.color = Color.yellow;
            }
            else
            {
                ratingText.text = "Слишком долго";
                ratingText.color = Color.red;
            }
        }

        if (winPanel != null)
            winPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    private string FormatTime(float t)
    {
        int minutes = (int)(t / 60f);
        float seconds = t % 60f;
        return minutes.ToString("00") + ":" + seconds.ToString("00.0");
    }

    public void GameOver()
    {
        if (finished) return;
        finished = true;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Pause()
    {
        if (finished) return;
        paused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        paused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void NextLevel()
    {
        int next = SceneManager.GetActiveScene().buildIndex + 1;

        if (next < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(next);
        else
            SceneManager.LoadScene(0); // последний уровень пройден: вернуться в начало
    }

}
