
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float gameTime;
    public bool gameActive;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        // for scene transition
        if (Instance != null && Instance != this)
            Destroy(this);
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        gameActive = true;
    }
    private void Update()
    {
        if (gameActive)
        {
            gameTime += Time.deltaTime;
            UIController.Instance.UpdateTimer(gameTime);
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Pause();
            }
        }
    }
    public void GameOver()
    {
        gameActive = false;
        UIController.Instance.gameOverPanel.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }
    public void Pause()
    {
        if (UIController.Instance.PausePanel.activeSelf == false && UIController.Instance.gameOverPanel.activeSelf == false)
        {
            UIController.Instance.PausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            UIController.Instance.PausePanel.SetActive(false);
            Time.timeScale = 1f;
        }


    }
    public void exit()
    {
        Application.Quit();

    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");

    }
}
