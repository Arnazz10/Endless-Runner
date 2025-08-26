# Unity Endless Runner - Detailed Setup Guide

## Prerequisites
- Unity 2022.3 LTS or newer
- Basic knowledge of Unity interface

## Step-by-Step Setup

### 1. Create New Unity Project
1. Open Unity Hub
2. Click "New Project"
3. Select "3D Core" template
4. Name it "EndlessRunner"
5. Click "Create project"

### 2. Import Scripts
1. In Unity, create a folder called "Scripts" in the Assets folder
2. Copy all the C# scripts from the Scripts folder into Unity's Scripts folder:
   - PlayerController.cs
   - ObstacleSpawner.cs
   - GameManager.cs
   - UIManager.cs

### 3. Set up the Scene

#### 3.1 Create Ground
1. Right-click in Hierarchy → 3D Object → Plane
2. Rename to "Ground"
3. In Inspector, set Transform:
   - Position: (0, 0, 0)
   - Scale: (10, 1, 50)
4. Add Rigidbody component:
   - Check "Use Gravity"
   - Check "Is Kinematic"
   - Check "Is Static"

#### 3.2 Create Player
1. Right-click in Hierarchy → 3D Object → Cube
2. Rename to "Player"
3. In Inspector, set Transform:
   - Position: (0, 1, 0)
   - Scale: (1, 1, 1)
4. Add Rigidbody component:
   - Check "Use Gravity"
   - Uncheck "Is Kinematic"
   - Mass: 1
   - Drag: 0
   - Angular Drag: 0.05
5. Add Box Collider component
6. Add PlayerController script
7. In PlayerController settings:
   - Run Speed: 10
   - Jump Force: 10
   - Slide Duration: 0.5
   - Slide Height: 0.5
8. Set Tag to "Player" (Create if doesn't exist)

#### 3.3 Create Obstacle Prefab
1. Right-click in Hierarchy → 3D Object → Cube
2. Rename to "Obstacle"
3. In Inspector, set Transform:
   - Position: (0, 1, 0)
   - Scale: (1, 2, 1)
4. Add Rigidbody component:
   - Check "Use Gravity"
   - Check "Is Kinematic"
   - Check "Is Static"
5. Add Box Collider component
6. Set Tag to "Obstacle" (Create if doesn't exist)
7. Drag from Hierarchy to Assets folder to create prefab
8. Delete from scene

### 4. Set up UI

#### 4.1 Create Canvas
1. Right-click in Hierarchy → UI → Canvas
2. Rename to "GameCanvas"
3. In Canvas component:
   - Render Mode: Screen Space - Overlay
   - UI Scale Mode: Scale With Screen Size
   - Reference Resolution: 1920 x 1080

#### 4.2 Create Game UI Panel
1. Right-click on Canvas → UI → Panel
2. Rename to "GameUIPanel"
3. Set Anchor to top-left
4. Set Rect Transform:
   - Width: 300
   - Height: 100
   - Position: (20, -20, 0)

#### 4.3 Create Score Text
1. Right-click on GameUIPanel → UI → Text
2. Rename to "ScoreText"
3. Set Text component:
   - Text: "Score: 0"
   - Font Size: 24
   - Color: White
   - Alignment: Left
4. Set Rect Transform:
   - Width: 280
   - Height: 30
   - Position: (10, -10, 0)

#### 4.4 Create Game Over Panel
1. Right-click on Canvas → UI → Panel
2. Rename to "GameOverPanel"
3. Set Anchor to center
4. Set Rect Transform:
   - Width: 400
   - Height: 300
   - Position: (0, 0, 0)
5. Initially set Active to false

#### 4.5 Create Game Over Text
1. Right-click on GameOverPanel → UI → Text
2. Rename to "GameOverText"
3. Set Text component:
   - Text: "Game Over!\nFinal Score: 0\nPress R to restart"
   - Font Size: 28
   - Color: White
   - Alignment: Center
4. Set Rect Transform:
   - Width: 380
   - Height: 150
   - Position: (0, 50, 0)

#### 4.6 Create Restart Button
1. Right-click on GameOverPanel → UI → Button
2. Rename to "RestartButton"
3. Set Button component:
   - Text: "Restart"
   - Font Size: 20
4. Set Rect Transform:
   - Width: 120
   - Height: 40
   - Position: (0, -50, 0)

### 5. Create Game Objects

#### 5.1 Create Game Manager
1. Right-click in Hierarchy → Create Empty
2. Rename to "GameManager"
3. Add GameManager script
4. In GameManager settings:
   - Player: Drag Player from Hierarchy
   - UIManager: Will be set in next step
   - ObstacleSpawner: Will be set in next step
   - Score Multiplier: 1
   - Auto Restart: false
   - Restart Delay: 3

#### 5.2 Create Obstacle Spawner
1. Right-click in Hierarchy → Create Empty
2. Rename to "ObstacleSpawner"
3. Add ObstacleSpawner script
4. In ObstacleSpawner settings:
   - Obstacle Prefab: Drag Obstacle prefab from Assets
   - Player: Drag Player from Hierarchy
   - Min Spawn Interval: 1.5
   - Max Spawn Interval: 3.0
   - Spawn Distance: 50
   - Lane Width: 3
   - Max Obstacles: 10
   - Cleanup Distance: 20

### 6. Configure Script References

#### 6.1 Configure Game Manager
1. Select GameManager in Hierarchy
2. In GameManager component:
   - Player: Drag Player from Hierarchy
   - UIManager: Drag Canvas from Hierarchy
   - ObstacleSpawner: Drag ObstacleSpawner from Hierarchy

#### 6.2 Configure UI Manager
1. Select Canvas in Hierarchy
2. Add UIManager script
3. In UIManager component:
   - Score Text: Drag ScoreText from Hierarchy
   - Game Over Text: Drag GameOverText from Hierarchy
   - Restart Button: Drag RestartButton from Hierarchy
   - Game Over Panel: Drag GameOverPanel from Hierarchy
   - Game UI Panel: Drag GameUIPanel from Hierarchy

#### 6.3 Configure Obstacle Spawner
1. Select ObstacleSpawner in Hierarchy
2. In ObstacleSpawner component:
   - Obstacle Prefab: Drag Obstacle prefab from Assets
   - Player: Drag Player from Hierarchy

### 7. Set up Camera
1. Select Main Camera in Hierarchy
2. Set Transform:
   - Position: (0, 5, -10)
   - Rotation: (15, 0, 0)
3. In Camera component:
   - Field of View: 60
   - Near Clip Plane: 0.3
   - Far Clip Plane: 1000

### 8. Test the Game
1. Press Play button
2. Player should auto-run forward
3. Press Spacebar to jump
4. Press Down Arrow or S to slide
5. Obstacles should spawn randomly
6. Colliding with obstacles should trigger game over
7. Press R to restart

## Troubleshooting

### Common Issues:
1. **Player doesn't move**: Check if PlayerController script is attached and Rigidbody is not kinematic
2. **Obstacles don't spawn**: Check ObstacleSpawner references and obstacle prefab
3. **UI doesn't update**: Check UIManager references and GameManager connections
4. **Collisions don't work**: Ensure objects have proper tags ("Player", "Obstacle")

### Performance Tips:
1. Limit max obstacles in ObstacleSpawner
2. Use object pooling for obstacles in larger games
3. Adjust cleanup distance based on performance needs

## Customization Options

### Player Movement:
- Adjust run speed in PlayerController
- Modify jump force and slide duration
- Change ground check distance

### Obstacle Spawning:
- Modify spawn intervals for difficulty
- Adjust lane width for wider/narrower paths
- Enable/disable different obstacle types

### Scoring:
- Change score multiplier in GameManager
- Modify score calculation formula
- Add bonus points for special achievements

### UI:
- Customize text formats in UIManager
- Change colors and fonts
- Add additional UI elements
