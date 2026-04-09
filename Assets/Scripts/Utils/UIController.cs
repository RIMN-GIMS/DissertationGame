using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [SerializeField] private Slider playerHealthSlide;
    [SerializeField] private TMP_Text playerHealthText;
    [SerializeField] private Slider xPSlide;
    [SerializeField] private TMP_Text XpText;
    public GameObject gameOverPanel;
    public GameObject XpMultPanel;
    public GameObject PausePanel;
    public GameObject LevelPanel;
    public LevelUpBtn[] levelUpButtons;
    [SerializeField] private TMP_Text timerText;
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
        XpMultMenuO();
    }
    public void UpdatePHealthUI()
    {
        playerHealthSlide.maxValue = PlayerController.Instance.playerMaxHealth;
        playerHealthSlide.value = PlayerController.Instance.playerCurHealth;
        playerHealthText.text = playerHealthSlide.value + " / " + playerHealthSlide.maxValue;
    }
    public void UpdateXPUI()
    {
        xPSlide.maxValue = PlayerController.Instance.playerLevels[PlayerController.Instance.currentLevel];
        xPSlide.value = PlayerController.Instance.experience;
        XpText.text = xPSlide.value + " / " + xPSlide.maxValue;
    }
    public void UpdateTimer(float timer)
    {
        float min = Mathf.FloorToInt(timer / 60f);
        float sec = Mathf.FloorToInt(timer % 60f);
        timerText.text = min + ":" + sec.ToString("00");
    }
    public void LeveUpMenuO()
    {
        LevelPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void LeveUpMenuC()
    {
        LevelPanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void XpMultMenuO()
    {
        XpMultPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void XpMultMenuC()
    {
        XpMultPanel.SetActive(false);
        Time.timeScale = 1f;
    }

}
