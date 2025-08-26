using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI References")]
    public Text scoreText;
    public Text gameOverText;
    public Button restartButton;
    public GameObject gameOverPanel;
    public GameObject gameUIPanel;
    
    [Header("UI Settings")]
    public string scoreFormat = "Score: {0:F0}";
    public string gameOverFormat = "Game Over!\nFinal Score: {0:F0}\nPress R to restart";
    public string restartButtonText = "Restart";
    
    // Private variables
    private GameManager gameManager;
    
    void Start()
    {
        InitializeUI();
        SetupEventListeners();
    }
    
    void InitializeUI()
    {
        // Find GameManager
        gameManager = FindObjectOfType<GameManager>();
        
        // Find UI elements if not assigned
        if (scoreText == null)
        {
            scoreText = GameObject.Find("ScoreText")?.GetComponent<Text>();
        }
        
        if (gameOverText == null)
        {
            gameOverText = GameObject.Find("GameOverText")?.GetComponent<Text>();
        }
        
        if (restartButton == null)
        {
            restartButton = GameObject.Find("RestartButton")?.GetComponent<Button>();
        }
        
        if (gameOverPanel == null)
        {
            gameOverPanel = GameObject.Find("GameOverPanel");
        }
        
        if (gameUIPanel == null)
        {
            gameUIPanel = GameObject.Find("GameUIPanel");
        }
        
        // Set initial UI state
        ShowGameUI();
        HideGameOver();
    }
    
    void SetupEventListeners()
    {
        // Subscribe to GameManager events
        if (gameManager != null)
        {
            gameManager.OnGameStart += OnGameStart;
            gameManager.OnGameOver += OnGameOver;
            gameManager.OnGameRestart += OnGameRestart;
        }
        
        // Setup restart button
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(OnRestartButtonClicked);
        }
    }
    
    public void UpdateScore(float score)
    {
        if (scoreText != null)
        {
            scoreText.text = string.Format(scoreFormat, score);
        }
    }
    
    public void ShowGameUI()
    {
        if (gameUIPanel != null)
        {
            gameUIPanel.SetActive(true);
        }
        
        if (scoreText != null)
        {
            scoreText.gameObject.SetActive(true);
        }
    }
    
    public void HideGameUI()
    {
        if (gameUIPanel != null)
        {
            gameUIPanel.SetActive(false);
        }
        
        if (scoreText != null)
        {
            scoreText.gameObject.SetActive(false);
        }
    }
    
    public void ShowGameOver(float finalScore)
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        
        if (gameOverText != null)
        {
            gameOverText.text = string.Format(gameOverFormat, finalScore);
            gameOverText.gameObject.SetActive(true);
        }
        
        if (restartButton != null)
        {
            restartButton.gameObject.SetActive(true);
        }
    }
    
    public void HideGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
        }
        
        if (restartButton != null)
        {
            restartButton.gameObject.SetActive(false);
        }
    }
    
    void OnGameStart()
    {
        ShowGameUI();
        HideGameOver();
        UpdateScore(0f);
    }
    
    void OnGameOver()
    {
        HideGameUI();
        ShowGameOver(gameManager.CurrentScore);
    }
    
    void OnGameRestart()
    {
        // UI will be updated by OnGameStart
    }
    
    void OnRestartButtonClicked()
    {
        if (gameManager != null)
        {
            gameManager.RestartGame();
        }
    }
    
    void OnDestroy()
    {
        // Unsubscribe from events
        if (gameManager != null)
        {
            gameManager.OnGameStart -= OnGameStart;
            gameManager.OnGameOver -= OnGameOver;
            gameManager.OnGameRestart -= OnGameRestart;
        }
        
        // Remove button listener
        if (restartButton != null)
        {
            restartButton.onClick.RemoveListener(OnRestartButtonClicked);
        }
    }
}
