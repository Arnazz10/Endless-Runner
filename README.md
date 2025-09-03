# Unity Endless Runner Game

A simple endless runner game built in Unity using C# where the player automatically runs forward and must avoid obstacles by jumping or sliding.

## Game Features

- **Auto-running player** with constant forward movement
- **Jump mechanics** (Spacebar)
- **Slide mechanics** (Down Arrow or S key)
- **Random obstacle spawning** with varying difficulty
- **Collision detection** for game over
- **Distance-based scoring system**

## Project Structure

```
Assets/
├── Scripts/
│   ├── PlayerController.cs
│   ├── ObstacleSpawner.cs
│   ├── GameManager.cs
│   └── UIManager.cs
├── Prefabs/
│   ├── Player.prefab
│   ├── Obstacle.prefab
│   └── Ground.prefab
└── Scenes/
    └── GameScene.unity
```

## Setup Instructions

### 1. Create the Scene
1. Create a new 3D scene in Unity
2. Save it as "GameScene" in the Scenes folder

### 2. Set up the Ground
1. Create a Plane (GameObject → 3D Object → Plane)
2. Scale it to (10, 1, 50) for a long running surface
3. Position it at (0, 0, 0)
4. Add a Rigidbody component and set it to "Static"

### 3. Create the Player
1. Create a Cube (GameObject → 3D Object → Cube)
2. Rename it to "Player"
3. Position it at (0, 1, 0)
4. Add a Rigidbody component
5. Add a Box Collider component
6. Attach the `PlayerController.cs` script
7. Create a prefab from this object

### 4. Create Obstacles
1. Create a Cube (GameObject → 3D Object → Cube)
2. Rename it to "Obstacle"
3. Scale it to (1, 2, 1) for a tall obstacle
4. Add a Rigidbody component and set it to "Static"
5. Add a Box Collider component
6. Create a prefab from this object

### 5. Set up the UI
1. Create a Canvas (GameObject → UI → Canvas)
2. Add a Text element for the score (UI → Text)
3. Add a Text element for game over message (UI → Text)
4. Add a Button for restart (UI → Button)
5. Attach the `UIManager.cs` script to the Canvas

### 6. Create Game Manager
1. Create an empty GameObject
2. Rename it to "GameManager"
3. Attach the `GameManager.cs` script

### 7. Create Obstacle Spawner
1. Create an empty GameObject
2. Rename it to "ObstacleSpawner"
3. Attach the `ObstacleSpawner.cs` script

### 8. Configure Script References
1. In GameManager, assign references to:
   - Player GameObject
   - UIManager script
   - ObstacleSpawner script
2. In ObstacleSpawner, assign:
   - Obstacle prefab
   - Player GameObject
3. In UIManager, assign:
   - Score Text component
   - Game Over Text component
   - Restart Button component

## Controls

- **Spacebar**: Jump
- **Down Arrow** or **S**: Slide
- **R**: Restart game (when game over)

## Script Overview

### PlayerController.cs
Handles player movement, jumping, sliding, and collision detection.

### ObstacleSpawner.cs
Manages obstacle spawning with random intervals and positions.

### GameManager.cs
Controls game state, scoring, and overall game flow.

### UIManager.cs
Manages UI elements and displays score/game over information.

## Game Mechanics

- Player moves forward automatically at constant speed
- Obstacles spawn randomly ahead of the player
- Collision with obstacles triggers game over
- Score increases based on distance traveled
- Game can be restarted after game over

## Customization

You can easily modify:
- Player speed in PlayerController
- Jump height and slide duration in PlayerController
- Obstacle spawn rate and types in ObstacleSpawner
- Scoring system in GameManager
- UI appearance in UIManager
