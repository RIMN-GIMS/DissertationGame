using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
   public static UIController Instance;

    [SerializeField] private Slider playerHealthSlide;
    [SerializeField] private TMP_Text playerHealthText;
    public GameObject gameOverPanel;
    public GameObject PausePanel;
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
    public void UpdatePHealthUI()
    {
        playerHealthSlide.maxValue = PlayerController.Instance.playerMaxHealth;
        playerHealthSlide.value = PlayerController.Instance.playerCurHealth;
        playerHealthText.text = playerHealthSlide.value + " / " + playerHealthSlide.maxValue;
    }
    public void UpdateTimer(float timer)
    {
        float min = Mathf.FloorToInt(timer/ 60f);
        float sec = Mathf.FloorToInt(timer % 60f);
    timerText.text = min + ":" + sec.ToString("00");
    }
}
