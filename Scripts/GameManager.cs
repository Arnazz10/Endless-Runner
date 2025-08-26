using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game References")]
    public GameObject player;
    public UIManager uiManager;
    public ObstacleSpawner obstacleSpawner;
    
    [Header("Game Settings")]
    public float scoreMultiplier = 1f;
    public bool autoRestart = false;
    public float restartDelay = 3f;
    
    // Private variables
    private bool isGameActive = false;
    private bool isGameOver = false;
    private float gameStartTime;
    private float currentScore = 0f;
    private float restartTimer = 0f;
    
    // Events
    public System.Action OnGameStart;
    public System.Action OnGameOver;
    public System.Action OnGameRestart;
    
    // Properties
    public bool IsGameActive => isGameActive;
    public bool IsGameOver => isGameOver;
    public float CurrentScore => currentScore;
    
    void Start()
    {
        InitializeGame();
    }
    
    void Update()
    {
        if (isGameActive)
        {
            UpdateScore();
        }
        
        if (isGameOver)
        {
            HandleGameOverInput();
            UpdateRestartTimer();
        }
    }
    
    void InitializeGame()
    {
        // Find references if not assigned
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        
        if (uiManager == null)
        {
            uiManager = FindObjectOfType<UIManager>();
        }
        
        if (obstacleSpawner == null)
        {
            obstacleSpawner = FindObjectOfType<ObstacleSpawner>();
        }
        
        // Subscribe to player death event
        if (player != null)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.OnPlayerDeath += HandlePlayerDeath;
            }
        }
        
        // Start the game
        StartGame();
    }
    
    void StartGame()
    {
        isGameActive = true;
        isGameOver = false;
        gameStartTime = Time.time;
        currentScore = 0f;
        restartTimer = 0f;
        
        // Reset player position
        if (player != null)
        {
            player.transform.position = new Vector3(0, 1, 0);
            player.transform.rotation = Quaternion.identity;
            
            // Reset player physics
            Rigidbody playerRb = player.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                playerRb.velocity = Vector3.zero;
                playerRb.angularVelocity = Vector3.zero;
            }
        }
        
        // Clear obstacles
        if (obstacleSpawner != null)
        {
            obstacleSpawner.ClearAllObstacles();
        }
        
        // Update UI
        if (uiManager != null)
        {
            uiManager.ShowGameUI();
            uiManager.UpdateScore(currentScore);
        }
        
        OnGameStart?.Invoke();
    }
    
    void UpdateScore()
    {
        if (player != null)
        {
            // Score based on distance traveled
            float distance = player.transform.position.z;
            currentScore = distance * scoreMultiplier;
            
            // Update UI
            if (uiManager != null)
            {
                uiManager.UpdateScore(currentScore);
            }
        }
    }
    
    void HandlePlayerDeath()
    {
        if (isGameOver) return;
        
        GameOver();
    }
    
    void GameOver()
    {
        isGameActive = false;
        isGameOver = true;
        restartTimer = 0f;
        
        // Update UI
        if (uiManager != null)
        {
            uiManager.ShowGameOver(currentScore);
        }
        
        OnGameOver?.Invoke();
    }
    
    void HandleGameOverInput()
    {
        // Manual restart with R key
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }
    
    void UpdateRestartTimer()
    {
        if (autoRestart)
        {
            restartTimer += Time.deltaTime;
            if (restartTimer >= restartDelay)
            {
                RestartGame();
            }
        }
    }
    
    public void RestartGame()
    {
        OnGameRestart?.Invoke();
        StartGame();
    }
    
    public void PauseGame()
    {
        if (isGameActive && !isGameOver)
        {
            Time.timeScale = 0f;
        }
    }
    
    public void ResumeGame()
    {
        if (isGameActive && !isGameOver)
        {
            Time.timeScale = 1f;
        }
    }
    
    void OnDestroy()
    {
        // Unsubscribe from events
        if (player != null)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.OnPlayerDeath -= HandlePlayerDeath;
            }
        }
    }
}
