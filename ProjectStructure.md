# Unity Endless Runner - Project Structure

## Directory Structure

```
Endless/
├── README.md                           # Main project documentation
├── UnitySetupGuide.md                  # Detailed Unity setup instructions
├── ProjectStructure.md                 # This file - project organization
├── Scripts/                            # All C# scripts
│   ├── PlayerController.cs             # Player movement, jump, slide
│   ├── ObstacleSpawner.cs              # Basic obstacle spawning
│   ├── ObstacleSpawnerPooled.cs        # Advanced spawning with object pooling
│   ├── GameManager.cs                  # Game state and flow control
│   ├── UIManager.cs                    # UI management and display
│   └── ObjectPool.cs                   # Object pooling for performance
└── UnityProject/                       # Unity project folder (when created)
    ├── Assets/
    │   ├── Scripts/                    # Copy scripts here
    │   ├── Prefabs/                    # Game object prefabs
    │   ├── Scenes/                     # Unity scenes
    │   └── Materials/                  # Materials and textures
    └── ProjectSettings/                # Unity project settings
```

## Script Descriptions

### Core Game Scripts

#### PlayerController.cs
**Purpose**: Handles all player movement and input
**Key Features**:
- Auto-forward movement at constant speed
- Jump mechanics (Spacebar)
- Slide mechanics (Down Arrow/S key)
- Ground detection
- Collision handling with obstacles
- Event-driven death system

**Configurable Parameters**:
- `runSpeed`: Forward movement speed
- `jumpForce`: Jump strength
- `slideDuration`: How long slide lasts
- `slideHeight`: Collider height during slide
- `groundCheckDistance`: Distance for ground detection

#### GameManager.cs
**Purpose**: Central game state management
**Key Features**:
- Game state tracking (active/over/restart)
- Score calculation based on distance
- Event system for game state changes
- Player death handling
- Restart functionality

**Configurable Parameters**:
- `scoreMultiplier`: Score calculation multiplier
- `autoRestart`: Automatic restart after game over
- `restartDelay`: Delay before auto-restart

#### UIManager.cs
**Purpose**: User interface management
**Key Features**:
- Score display and updates
- Game over screen management
- Restart button functionality
- Event-driven UI updates

**Configurable Parameters**:
- `scoreFormat`: Score display format
- `gameOverFormat`: Game over message format
- UI element references

### Obstacle System

#### ObstacleSpawner.cs (Basic)
**Purpose**: Simple obstacle spawning system
**Key Features**:
- Random obstacle spawning
- Multiple obstacle types (high, low, wide)
- Automatic cleanup of old obstacles
- Configurable spawn intervals

#### ObstacleSpawnerPooled.cs (Advanced)
**Purpose**: Performance-optimized obstacle spawning
**Key Features**:
- Object pooling for better performance
- Dynamic difficulty scaling
- Progressive spawn rate increases
- Memory-efficient obstacle management

**Additional Features**:
- `enableDifficultyScaling`: Progressive difficulty
- `difficultyIncreaseInterval`: Time between difficulty increases
- `maxSpeedMultiplier`: Maximum difficulty multiplier

#### ObjectPool.cs
**Purpose**: Object pooling system for performance
**Key Features**:
- Reusable object pools
- Automatic object recycling
- Memory-efficient object management
- Configurable pool sizes

### Script Dependencies

```
GameManager
├── PlayerController (for death events)
├── UIManager (for score/game over updates)
└── ObstacleSpawner (for obstacle cleanup)

PlayerController
└── GameManager (for game over events)

ObstacleSpawner
├── GameManager (for game over events)
└── ObjectPool (if using pooled version)

UIManager
└── GameManager (for game state events)
```

## Unity Scene Hierarchy

```
Scene
├── Main Camera
├── Directional Light
├── Ground (Plane)
├── Player (Cube with PlayerController)
├── GameManager (Empty GameObject)
├── ObstacleSpawner (Empty GameObject)
├── ObjectPool (Empty GameObject) - Optional
└── GameCanvas (Canvas)
    ├── GameUIPanel
    │   └── ScoreText
    └── GameOverPanel
        ├── GameOverText
        └── RestartButton
```

## Tags and Layers

### Required Tags
- `"Player"`: Player GameObject
- `"Obstacle"`: Obstacle GameObjects

### Recommended Layers
- `"Default"`: General objects
- `"Ground"`: Ground plane (for ground detection)

## Performance Considerations

### Basic Setup
- Use `ObstacleSpawner.cs` for simple games
- Limit `maxObstacles` to prevent memory issues
- Adjust `cleanupDistance` based on performance

### Advanced Setup
- Use `ObstacleSpawnerPooled.cs` for better performance
- Configure `ObjectPool.cs` with appropriate pool sizes
- Enable difficulty scaling for progressive challenge

### Optimization Tips
1. **Object Pooling**: Use pooled spawner for games with many obstacles
2. **Cleanup Distance**: Balance between memory usage and visual continuity
3. **Max Obstacles**: Limit based on target platform performance
4. **Difficulty Scaling**: Gradually increase challenge to maintain engagement

## Customization Guide

### Player Movement
```csharp
// In PlayerController
public float runSpeed = 10f;        // Forward speed
public float jumpForce = 10f;       // Jump strength
public float slideDuration = 0.5f;  // Slide time
```

### Obstacle Spawning
```csharp
// In ObstacleSpawner
public float minSpawnInterval = 1.5f;  // Minimum time between spawns
public float maxSpawnInterval = 3.0f;  // Maximum time between spawns
public float laneWidth = 3f;           // Spawn area width
```

### Difficulty Scaling
```csharp
// In ObstacleSpawnerPooled
public bool enableDifficultyScaling = true;
public float difficultyIncreaseInterval = 30f;  // Seconds between increases
public float maxSpeedMultiplier = 2f;           // Maximum difficulty
```

### UI Customization
```csharp
// In UIManager
public string scoreFormat = "Score: {0:F0}";
public string gameOverFormat = "Game Over!\nFinal Score: {0:F0}";
```

## File Organization Best Practices

1. **Scripts Folder**: Keep all C# scripts organized
2. **Prefabs**: Create reusable prefabs for obstacles and player
3. **Materials**: Organize materials by type (player, obstacles, ground)
4. **Scenes**: Separate scenes for different game states
5. **Documentation**: Keep setup guides and documentation updated

## Version Control

### Recommended .gitignore for Unity
```
# Unity generated files
[Ll]ibrary/
[Tt]emp/
[Oo]bj/
[Bb]uild/
[Bb]uilds/
[Ll]ogs/
[Uu]ser[Ss]ettings/

# VS/Rider/MD/Consulo generated files
.vs/
.idea/
ExportedObj/
.consulo/
*.csproj
*.unityproj
*.sln
*.suo
*.tmp
*.user
*.userprefs
*.pidb
*.booproj
*.svd
*.pdb
*.mdb
*.opendb
*.VC.db

# Unity3D generated meta files
*.pidb.meta
*.pdb.meta
*.mdb.meta

# Unity3D generated file on crash reports
sysinfo.txt

# Builds
*.apk
*.aab
*.unitypackage
*.app

# Crashlytics generated file
crashlytics-build.properties
```

## Troubleshooting

### Common Issues and Solutions

1. **Player doesn't move**
   - Check Rigidbody settings (not kinematic)
   - Verify PlayerController script is attached
   - Ensure runSpeed > 0

2. **Obstacles don't spawn**
   - Check ObstacleSpawner references
   - Verify obstacle prefab exists
   - Check spawn intervals are reasonable

3. **UI doesn't update**
   - Verify UIManager references
   - Check GameManager connections
   - Ensure UI elements are active

4. **Performance issues**
   - Use object pooling for obstacle spawning
   - Reduce max obstacles
   - Increase cleanup distance
   - Optimize obstacle prefabs

5. **Collision detection problems**
   - Verify tags are set correctly
   - Check collider components
   - Ensure proper layer settings
