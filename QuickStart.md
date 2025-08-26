# 🚀 Quick Start - Get the Game Running in 5 Minutes

## Prerequisites
- Unity 2022.3 LTS or newer installed
- Basic Unity knowledge

## Step 1: Create Unity Project
1. Open Unity Hub
2. Click "New Project"
3. Select "3D Core" template
4. Name it "EndlessRunner"
5. Click "Create project"

## Step 2: Import Scripts
1. In Unity Project window, right-click in Assets → Create → Folder
2. Name it "Scripts"
3. Copy all `.cs` files from the Scripts folder into Unity's Scripts folder

## Step 3: Quick Scene Setup (Copy-Paste Method)

### 3.1 Create Basic Scene Objects
1. **Delete the default Cube** in the scene
2. **Create Ground**: Right-click Hierarchy → 3D Object → Plane
   - Rename to "Ground"
   - Set Scale to (10, 1, 50)
   - Position at (0, 0, 0)

3. **Create Player**: Right-click Hierarchy → 3D Object → Cube
   - Rename to "Player"
   - Position at (0, 1, 0)
   - Add Rigidbody component (check "Use Gravity")
   - Add Box Collider component
   - Set Tag to "Player" (Create if needed)

4. **Create Obstacle Prefab**:
   - Right-click Hierarchy → 3D Object → Cube
   - Rename to "Obstacle"
   - Set Scale to (1, 2, 1)
   - Position at (0, 1, 0)
   - Add Rigidbody component (check "Is Kinematic", "Is Static")
   - Add Box Collider component
   - Set Tag to "Obstacle" (Create if needed)
   - Drag to Assets folder to create prefab
   - Delete from scene

### 3.2 Create UI
1. **Create Canvas**: Right-click Hierarchy → UI → Canvas
2. **Create Score Text**: Right-click Canvas → UI → Text
   - Rename to "ScoreText"
   - Set Text to "Score: 0"
   - Position at top-left (20, -20, 0)
   - Set Font Size to 24

3. **Create Game Over Panel**: Right-click Canvas → UI → Panel
   - Rename to "GameOverPanel"
   - Set Active to false
   - Position at center

4. **Create Game Over Text**: Right-click GameOverPanel → UI → Text
   - Rename to "GameOverText"
   - Set Text to "Game Over!\nFinal Score: 0\nPress R to restart"
   - Set Font Size to 28
   - Set Alignment to Center

5. **Create Restart Button**: Right-click GameOverPanel → UI → Button
   - Rename to "RestartButton"
   - Set Text to "Restart"

### 3.3 Create Game Objects
1. **Create GameManager**: Right-click Hierarchy → Create Empty
   - Rename to "GameManager"

2. **Create ObstacleSpawner**: Right-click Hierarchy → Create Empty
   - Rename to "ObstacleSpawner"

## Step 4: Attach Scripts
1. **Player**: Select Player → Add Component → PlayerController
2. **GameManager**: Select GameManager → Add Component → GameManager
3. **ObstacleSpawner**: Select ObstacleSpawner → Add Component → ObstacleSpawner
4. **Canvas**: Select Canvas → Add Component → UIManager

## Step 5: Configure References
1. **GameManager** (select GameManager object):
   - Player: Drag Player from Hierarchy
   - UIManager: Drag Canvas from Hierarchy
   - ObstacleSpawner: Drag ObstacleSpawner from Hierarchy

2. **ObstacleSpawner** (select ObstacleSpawner object):
   - Obstacle Prefab: Drag Obstacle prefab from Assets
   - Player: Drag Player from Hierarchy

3. **UIManager** (select Canvas object):
   - Score Text: Drag ScoreText from Hierarchy
   - Game Over Text: Drag GameOverText from Hierarchy
   - Restart Button: Drag RestartButton from Hierarchy
   - Game Over Panel: Drag GameOverPanel from Hierarchy

## Step 6: Set up Camera
1. Select Main Camera
2. Set Position to (0, 5, -10)
3. Set Rotation to (15, 0, 0)

## Step 7: Test the Game
1. Press Play button
2. Player should auto-run forward
3. Press **Spacebar** to jump
4. Press **Down Arrow** or **S** to slide
5. Obstacles should spawn randomly
6. Collide with obstacles to trigger game over
7. Press **R** to restart

## 🎮 Controls
- **Spacebar**: Jump
- **Down Arrow** or **S**: Slide
- **R**: Restart (when game over)

## 🐛 Quick Troubleshooting
- **Player not moving**: Check if PlayerController script is attached
- **No obstacles**: Check ObstacleSpawner references
- **No UI updates**: Check UIManager references
- **No collisions**: Ensure tags are set ("Player", "Obstacle")

## 🎯 Success Indicators
✅ Player auto-runs forward  
✅ Spacebar makes player jump  
✅ Down Arrow/S makes player slide  
✅ Obstacles spawn randomly  
✅ Collision with obstacles triggers game over  
✅ Score increases with distance  
✅ R key restarts the game  

If all these work, your game is running successfully! 🎉
