# Reflect - AI Coding Instructions

## Project Overview
Unity 2021.3.33f1 puzzle game where players reflect lasers using mirrors to hit goal targets. Game involves placing mirrors on a grid, rotating them, and spawning lasers that bounce via physics.

## Architecture

### Singleton Game Manager
`Main.cs` uses singleton pattern (`Main.Manage`) for centralized state management:
- Tracks mirror placement count (`maxMirrors` limit enforced before play)
- Tracks goal completion (`numGoals` vs. goals hit)
- Controls game state (`isPlaying` blocks mirror placement/rotation after lasers spawn)
- Owns `turretPrefabs` list for laser spawning

Always access game state through `Main.Manage` - never create independent state trackers.

### Laser Physics System
Lasers use `VolumetricLineBehavior.cs` (volumetric line rendering):
- **Growth mechanism**: Lasers grow upward in `Update()` by increasing `StartPos.y` at `laserSpeed` until hitting something
- **State management**: Critical bug prevention through reset pattern in `Start()` - always reset `isHit`, `m_canTrigger`, and `m_startPos = m_endPos` to prevent stale state
- **Collision timing**: Uses 0.1s grace period (`EnableTriggersAfterDelay`) to prevent immediate self-collision
- **Reflection logic**: `OnTriggerEnter()` handles three collision types:
  - `Mirror`: Spawns new laser based on `MirrorType` (Reflect = Vector3.Reflect, Redirect = use normal directly)
  - `wall`: Stops laser growth
  - `Goal`: Calls `Goal.GoalHit()` to update materials and notify Main

### Mirror System
Mirrors have two behavioral states determined by rotation:
- **MirrorType.Reflect** (0°, 90°, 180°, 270°): Uses `Vector3.Reflect()` for realistic bouncing
- **MirrorType.Redirect** (45°, 135°, 225°, 315°): Sends laser along `surfaceNormal` direction

`MirrorRotation.cs` updates `parentMirror.mirrorType` based on Y-axis rotation modulo 90°. The rotation script is on a child collider for easier clicking while parent contains the actual mirror mesh.

### Placement System
`spawnMirror.cs` attached to grid tiles:
- **Left-click**: Spawns mirror if `!Main.Manage.MirrorCountFull()` and `!Main.Manage.IsPlaying()`
- **Right-click**: Destroys mirror and decrements count
- Stores `mirrorRef` to prevent duplicate placement on same tile

## Key Conventions

### Prefab Organization
- `Assets/__Scripts/` - All C# scripts with double underscore prefix
- `Assets/__Prefabs/` - Reusable game objects
- `Assets/__Materials/` - Materials with naming pattern `{name}_mat.mat`
- Scene organization: `Main Menu.unity`, `Level1.unity`, `Level2.unity`

### Editor Tools
- `[ContextMenu("Generate Grid")]` in `GridGenerator.cs` - Right-click inspector to generate tile grid
- `[ContextMenu("Spawn Lasers")]` in `Main.cs` - Testing spawning without UI
- Use `#if UNITY_EDITOR` to wrap editor-only code

### Game Flow
1. **Setup Phase**: Player places/rotates mirrors (click tiles, click mirrors)
2. **Play Phase**: Click Play button → calls `Main.SpawnLasers()` → sets `isPlaying = true` → lasers spawn and propagate
3. **Completion**: All goals hit → triggers end sequence
4. **Reset**: `Main.LevelReset()` reloads scene via SceneManager

### Common Patterns
- **Tag-based collision**: Check `CompareTag("Mirror")`, `CompareTag("wall")`, `CompareTag("Goal")`
- **Transform hierarchy**: Mirror rotation scripts on child objects with larger colliders for UX
- **Material swapping**: Goals change materials (`notHitMaterial` → `hitMaterial`) on activation
- **Coroutine animations**: `MirrorRotation.cs` uses `SmoothStep` for rotation interpolation

## Development Workflow

### Building for WebGL
WebGL builds output to `WebGL Builds/` directory. Project already configured for web deployment (see existing build artifacts).

### Testing
- Use Context Menu commands for quick iteration
- Test laser reflection by adjusting `surfaceNormal` vectors in Mirror components
- Debug rays drawn in Scene view (green = redirect, blue = reflect) with 10s duration

### Debugging Laser Issues
If lasers behave unexpectedly:
1. Check `isHit` and `m_canTrigger` state in `VolumetricLineBehavior.cs`
2. Verify `Start()` resets: `m_startPos = m_endPos` prevents "already long" bugs
3. Ensure 0.1s grace period completes before expecting triggers
4. Verify mirror tags and `MirrorType` matches rotation angle

## External Dependencies
- **VolumetricLines namespace**: Third-party volumetric line renderer by Sebastien Hillaire (see attribution in `VolumetricLineBehavior.cs`)
- Uses CapsuleCollider components for laser collision detection
