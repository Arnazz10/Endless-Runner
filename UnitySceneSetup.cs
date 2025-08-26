using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class UnitySceneSetup : EditorWindow
{
    [MenuItem("Tools/Setup Endless Runner Scene")]
    public static void SetupScene()
    {
        // Clear existing scene
        ClearScene();
        
        // Create ground
        GameObject ground = CreateGround();
        
        // Create player
        GameObject player = CreatePlayer();
        
        // Create obstacle prefab
        GameObject obstaclePrefab = CreateObstaclePrefab();
        
        // Create UI
        GameObject canvas = CreateUI();
        
        // Create game objects
        GameObject gameManager = CreateGameManager();
        GameObject obstacleSpawner = CreateObstacleSpawner();
        
        // Configure references
        ConfigureReferences(player, canvas, gameManager, obstacleSpawner, obstaclePrefab);
        
        // Setup camera
        SetupCamera();
        
        Debug.Log("Endless Runner scene setup complete! Press Play to test.");
    }
    
    static void ClearScene()
    {
        // Delete all objects except camera and light
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.name != "Main Camera" && obj.name != "Directional Light")
            {
                DestroyImmediate(obj);
            }
        }
    }
    
    static GameObject CreateGround()
    {
        GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ground.name = "Ground";
        ground.transform.position = Vector3.zero;
        ground.transform.localScale = new Vector3(10, 1, 50);
        
        // Add rigidbody
        Rigidbody groundRb = ground.AddComponent<Rigidbody>();
        groundRb.isKinematic = true;
        groundRb.useGravity = true;
        
        return ground;
    }
    
    static GameObject CreatePlayer()
    {
        GameObject player = GameObject.CreatePrimitive(PrimitiveType.Cube);
        player.name = "Player";
        player.transform.position = new Vector3(0, 1, 0);
        
        // Add rigidbody
        Rigidbody playerRb = player.AddComponent<Rigidbody>();
        playerRb.useGravity = true;
        playerRb.mass = 1f;
        playerRb.drag = 0f;
        playerRb.angularDrag = 0.05f;
        
        // Add collider (already added by CreatePrimitive)
        
        // Add PlayerController script
        player.AddComponent<PlayerController>();
        
        // Set tag
        player.tag = "Player";
        
        return player;
    }
    
    static GameObject CreateObstaclePrefab()
    {
        GameObject obstacle = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obstacle.name = "Obstacle";
        obstacle.transform.position = new Vector3(0, 1, 0);
        obstacle.transform.localScale = new Vector3(1, 2, 1);
        
        // Add rigidbody
        Rigidbody obstacleRb = obstacle.AddComponent<Rigidbody>();
        obstacleRb.isKinematic = true;
        obstacleRb.useGravity = true;
        
        // Set tag
        obstacle.tag = "Obstacle";
        
        // Create prefab
        string prefabPath = "Assets/Obstacle.prefab";
        GameObject prefab = PrefabUtility.SaveAsPrefabAsset(obstacle, prefabPath);
        
        // Delete from scene
        DestroyImmediate(obstacle);
        
        return prefab;
    }
    
    static GameObject CreateUI()
    {
        // Create Canvas
        GameObject canvas = new GameObject("Canvas");
        Canvas canvasComponent = canvas.AddComponent<Canvas>();
        canvasComponent.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.AddComponent<CanvasScaler>();
        canvas.AddComponent<GraphicRaycaster>();
        
        // Create Score Text
        GameObject scoreText = new GameObject("ScoreText");
        scoreText.transform.SetParent(canvas.transform);
        Text scoreTextComponent = scoreText.AddComponent<Text>();
        scoreTextComponent.text = "Score: 0";
        scoreTextComponent.fontSize = 24;
        scoreTextComponent.color = Color.white;
        scoreTextComponent.alignment = TextAnchor.UpperLeft;
        
        RectTransform scoreRect = scoreText.GetComponent<RectTransform>();
        scoreRect.anchorMin = new Vector2(0, 1);
        scoreRect.anchorMax = new Vector2(0, 1);
        scoreRect.anchoredPosition = new Vector2(20, -20);
        scoreRect.sizeDelta = new Vector2(200, 30);
        
        // Create Game Over Panel
        GameObject gameOverPanel = new GameObject("GameOverPanel");
        gameOverPanel.transform.SetParent(canvas.transform);
        Image panelImage = gameOverPanel.AddComponent<Image>();
        panelImage.color = new Color(0, 0, 0, 0.8f);
        
        RectTransform panelRect = gameOverPanel.GetComponent<RectTransform>();
        panelRect.anchorMin = new Vector2(0.5f, 0.5f);
        panelRect.anchorMax = new Vector2(0.5f, 0.5f);
        panelRect.anchoredPosition = Vector2.zero;
        panelRect.sizeDelta = new Vector2(400, 300);
        gameOverPanel.SetActive(false);
        
        // Create Game Over Text
        GameObject gameOverText = new GameObject("GameOverText");
        gameOverText.transform.SetParent(gameOverPanel.transform);
        Text gameOverTextComponent = gameOverText.AddComponent<Text>();
        gameOverTextComponent.text = "Game Over!\nFinal Score: 0\nPress R to restart";
        gameOverTextComponent.fontSize = 28;
        gameOverTextComponent.color = Color.white;
        gameOverTextComponent.alignment = TextAnchor.MiddleCenter;
        
        RectTransform gameOverRect = gameOverText.GetComponent<RectTransform>();
        gameOverRect.anchorMin = new Vector2(0.5f, 0.5f);
        gameOverRect.anchorMax = new Vector2(0.5f, 0.5f);
        gameOverRect.anchoredPosition = new Vector2(0, 50);
        gameOverRect.sizeDelta = new Vector2(380, 150);
        
        // Create Restart Button
        GameObject restartButton = new GameObject("RestartButton");
        restartButton.transform.SetParent(gameOverPanel.transform);
        Button buttonComponent = restartButton.AddComponent<Button>();
        Image buttonImage = restartButton.AddComponent<Image>();
        buttonImage.color = Color.white;
        
        GameObject buttonText = new GameObject("Text");
        buttonText.transform.SetParent(restartButton.transform);
        Text buttonTextComponent = buttonText.AddComponent<Text>();
        buttonTextComponent.text = "Restart";
        buttonTextComponent.fontSize = 20;
        buttonTextComponent.color = Color.black;
        buttonTextComponent.alignment = TextAnchor.MiddleCenter;
        
        RectTransform buttonRect = restartButton.GetComponent<RectTransform>();
        buttonRect.anchorMin = new Vector2(0.5f, 0.5f);
        buttonRect.anchorMax = new Vector2(0.5f, 0.5f);
        buttonRect.anchoredPosition = new Vector2(0, -50);
        buttonRect.sizeDelta = new Vector2(120, 40);
        
        RectTransform buttonTextRect = buttonText.GetComponent<RectTransform>();
        buttonTextRect.anchorMin = Vector2.zero;
        buttonTextRect.anchorMax = Vector2.one;
        buttonTextRect.anchoredPosition = Vector2.zero;
        buttonTextRect.sizeDelta = Vector2.zero;
        
        // Add UIManager script
        canvas.AddComponent<UIManager>();
        
        return canvas;
    }
    
    static GameObject CreateGameManager()
    {
        GameObject gameManager = new GameObject("GameManager");
        gameManager.AddComponent<GameManager>();
        return gameManager;
    }
    
    static GameObject CreateObstacleSpawner()
    {
        GameObject obstacleSpawner = new GameObject("ObstacleSpawner");
        obstacleSpawner.AddComponent<ObstacleSpawner>();
        return obstacleSpawner;
    }
    
    static void ConfigureReferences(GameObject player, GameObject canvas, GameObject gameManager, GameObject obstacleSpawner, GameObject obstaclePrefab)
    {
        // Configure GameManager
        GameManager gm = gameManager.GetComponent<GameManager>();
        gm.player = player;
        gm.uiManager = canvas.GetComponent<UIManager>();
        gm.obstacleSpawner = obstacleSpawner.GetComponent<ObstacleSpawner>();
        
        // Configure ObstacleSpawner
        ObstacleSpawner os = obstacleSpawner.GetComponent<ObstacleSpawner>();
        os.obstaclePrefab = obstaclePrefab;
        os.player = player;
        
        // Configure UIManager
        UIManager ui = canvas.GetComponent<UIManager>();
        ui.scoreText = canvas.transform.Find("ScoreText").GetComponent<Text>();
        ui.gameOverText = canvas.transform.Find("GameOverPanel/GameOverText").GetComponent<Text>();
        ui.restartButton = canvas.transform.Find("GameOverPanel/RestartButton").GetComponent<Button>();
        ui.gameOverPanel = canvas.transform.Find("GameOverPanel").gameObject;
    }
    
    static void SetupCamera()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            mainCamera.transform.position = new Vector3(0, 5, -10);
            mainCamera.transform.rotation = Quaternion.Euler(15, 0, 0);
        }
    }
}
