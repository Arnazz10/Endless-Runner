using UnityEngine;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject obstaclePrefab;
    public float minSpawnInterval = 1.5f;
    public float maxSpawnInterval = 3.0f;
    public float spawnDistance = 50f;
    public float laneWidth = 3f;
    public int maxObstacles = 10;
    
    [Header("Obstacle Types")]
    public bool spawnHighObstacles = true;
    public bool spawnLowObstacles = true;
    public bool spawnWideObstacles = false;
    
    // Private variables
    private float nextSpawnTime;
    private float currentSpawnInterval;
    private Transform playerTransform;
    private List<GameObject> activeObstacles = new List<GameObject>();
    private bool isGameOver = false;
    
    // Obstacle cleanup
    public float cleanupDistance = 20f;
    
    void Start()
    {
        // Find player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        
        // Set initial spawn time
        SetNextSpawnTime();
        
        // Subscribe to game over event
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.OnGameOver += HandleGameOver;
        }
    }
    
    void Update()
    {
        if (isGameOver) return;
        
        // Check if it's time to spawn
        if (Time.time >= nextSpawnTime)
        {
            SpawnObstacle();
            SetNextSpawnTime();
        }
        
        // Clean up obstacles that are too far behind
        CleanupObstacles();
    }
    
    void SetNextSpawnTime()
    {
        currentSpawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
        nextSpawnTime = Time.time + currentSpawnInterval;
    }
    
    void SpawnObstacle()
    {
        if (obstaclePrefab == null || playerTransform == null) return;
        
        // Calculate spawn position
        Vector3 spawnPosition = CalculateSpawnPosition();
        
        // Create obstacle
        GameObject obstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
        
        // Configure obstacle based on type
        ConfigureObstacle(obstacle);
        
        // Add to active obstacles list
        activeObstacles.Add(obstacle);
        
        // Remove from list if we exceed max obstacles
        if (activeObstacles.Count > maxObstacles)
        {
            GameObject oldestObstacle = activeObstacles[0];
            activeObstacles.RemoveAt(0);
            if (oldestObstacle != null)
            {
                Destroy(oldestObstacle);
            }
        }
    }
    
    Vector3 CalculateSpawnPosition()
    {
        // Spawn ahead of player
        float zPosition = playerTransform.position.z + spawnDistance;
        
        // Random lane position
        float xPosition = Random.Range(-laneWidth, laneWidth);
        
        // Y position (on ground)
        float yPosition = 1f; // Adjust based on obstacle height
        
        return new Vector3(xPosition, yPosition, zPosition);
    }
    
    void ConfigureObstacle(GameObject obstacle)
    {
        // Randomly choose obstacle type
        int obstacleType = Random.Range(0, 3);
        
        switch (obstacleType)
        {
            case 0: // High obstacle (jump over)
                if (spawnHighObstacles)
                {
                    obstacle.transform.localScale = new Vector3(1f, 2f, 1f);
                    obstacle.transform.position = new Vector3(obstacle.transform.position.x, 1f, obstacle.transform.position.z);
                }
                break;
                
            case 1: // Low obstacle (slide under)
                if (spawnLowObstacles)
                {
                    obstacle.transform.localScale = new Vector3(1f, 0.5f, 1f);
                    obstacle.transform.position = new Vector3(obstacle.transform.position.x, 0.25f, obstacle.transform.position.z);
                }
                break;
                
            case 2: // Wide obstacle (requires precise timing)
                if (spawnWideObstacles)
                {
                    obstacle.transform.localScale = new Vector3(2f, 1f, 1f);
                    obstacle.transform.position = new Vector3(obstacle.transform.position.x, 0.5f, obstacle.transform.position.z);
                }
                break;
        }
        
        // Ensure obstacle has proper tag
        obstacle.tag = "Obstacle";
    }
    
    void CleanupObstacles()
    {
        if (playerTransform == null) return;
        
        for (int i = activeObstacles.Count - 1; i >= 0; i--)
        {
            if (activeObstacles[i] == null)
            {
                activeObstacles.RemoveAt(i);
                continue;
            }
            
            // Check if obstacle is too far behind player
            float distanceBehind = playerTransform.position.z - activeObstacles[i].transform.position.z;
            if (distanceBehind > cleanupDistance)
            {
                Destroy(activeObstacles[i]);
                activeObstacles.RemoveAt(i);
            }
        }
    }
    
    void HandleGameOver()
    {
        isGameOver = true;
    }
    
    public void ClearAllObstacles()
    {
        foreach (GameObject obstacle in activeObstacles)
        {
            if (obstacle != null)
            {
                Destroy(obstacle);
            }
        }
        activeObstacles.Clear();
    }
    
    void OnDestroy()
    {
        // Unsubscribe from events
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.OnGameOver -= HandleGameOver;
        }
    }
}
