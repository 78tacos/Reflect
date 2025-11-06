# Reflect

A Unity clone of the Reflector game from Cool Math Games - a puzzle game where you must reflect light beams into targets using mirrors!

## About the Game

Reflect is a puzzle game that challenges players to use mirrors to redirect light beams from sources to targets. The game features:

- **Light Sources**: Emit beams of light in a specific direction
- **Mirrors**: Reflective surfaces that can be rotated by clicking and dragging
- **Targets**: Goals that must be illuminated by the light beams to complete the level
- **Multiple Reflections**: Light beams can bounce off multiple mirrors before reaching the target

## How to Play

1. **Objective**: Direct the light beam from the source to all targets to complete the level
2. **Controls**:
   - **Left Click + Drag** on mirrors to rotate them
   - **R Key**: Restart the current level
   - **N Key**: Go to next level (if available)
3. **Strategy**: 
   - Observe the light source direction
   - Plan the mirror angles needed to reach the targets
   - Adjust mirror rotations to fine-tune the beam path
   - All targets must be hit simultaneously to win

## Project Structure

```
Assets/
├── Scenes/
│   └── Level1.unity          # Sample level scene
├── Scripts/
│   ├── LightBeam.cs          # Handles light beam rendering and reflection physics
│   ├── LightSource.cs        # Emits light beams
│   ├── Mirror.cs             # Reflective mirror with rotation controls
│   ├── Target.cs             # Goal objects that detect light hits
│   ├── GameManager.cs        # Level management and win condition
│   └── UIManager.cs          # UI display updates
├── Prefabs/                  # (Add prefabs for reusable game objects)
└── Materials/                # (Add materials for visual appearance)
```

## Getting Started

### Prerequisites
- Unity 2022.3.10f1 or later
- Basic understanding of Unity Editor

### Opening the Project
1. Clone this repository
2. Open Unity Hub
3. Click "Add" and select the project folder
4. Open the project in Unity

### Creating a Level
1. Create a new scene or open `Assets/Scenes/Level1.unity`
2. Add a **GameManager** (Empty GameObject with GameManager script)
3. Add **Light Sources**:
   - Create a Cube or Sphere
   - Add the LightSource script
   - Set emission direction in the inspector
   - Tag as "LightSource"
4. Add **Mirrors**:
   - Create a Plane or Quad
   - Add the Mirror script
   - Add a Box Collider
   - Tag as "Mirror"
5. Add **Targets**:
   - Create a Sphere or Cube
   - Add the Target script
   - Add a Collider
   - Tag as "Target"
6. Position objects to create interesting puzzles!

## Features

### Core Mechanics
- ✅ Light beam ray casting with visual rendering
- ✅ Mirror reflection physics
- ✅ Multiple reflection support (up to 10 bounces)
- ✅ Target detection and highlighting
- ✅ Win condition checking
- ✅ Level restart and progression

### Player Controls
- ✅ Mouse-based mirror rotation
- ✅ Keyboard shortcuts for level control
- ✅ Visual feedback for active targets

## Technical Details

The game uses Unity's Physics system for raycasting and reflection:
- `Physics.Raycast()` for beam collision detection
- `Vector3.Reflect()` for calculating reflection angles
- `LineRenderer` for visualizing light beams
- Tag-based object identification for mirrors and targets

## Future Enhancements

Potential features to add:
- Multiple light colors and color-specific targets
- Prisms that split light into multiple beams
- Obstacles that block light
- Time-based challenges
- Level editor
- More complex puzzles with multiple light sources
- Sound effects and background music
- Particle effects for light hits

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Credits

Inspired by the Reflector game from Cool Math Games.
Created as a Unity learning project.
