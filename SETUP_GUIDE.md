# Setup Guide - Creating Your First Level

This guide will walk you through creating a playable level in the Reflect game.

## Prerequisites

- Unity 2022.3.10f1 or later installed
- This repository cloned to your local machine

## Step 1: Open the Project

1. Open Unity Hub
2. Click "Add" and navigate to the project folder
3. Select the folder and click "Open"
4. Unity will import the project (this may take a few minutes)

## Step 2: Understanding the Scene Hierarchy

A typical level needs these components:

```
Scene
├── Main Camera
├── Directional Light
├── GameManager (Empty GameObject with GameManager script)
├── LightSource (Cube/Sphere with LightSource script)
├── Mirror(s) (Plane/Quad with Mirror script and Collider)
└── Target(s) (Sphere/Cube with Target script and Collider)
```

## Step 3: Create a New Level

### 3.1 Create the Scene
1. In Unity, go to `File > New Scene`
2. Choose "3D" template
3. Save as `Assets/Scenes/YourLevelName.unity`

### 3.2 Add the Game Manager
1. Create an empty GameObject: `GameObject > Create Empty`
2. Rename it to "GameManager"
3. Add the GameManager script: `Add Component > GameManager`
4. Configure settings in the Inspector:
   - **Next Level Name**: Name of the next scene (optional)
   - **Auto Advance**: Check to automatically load next level when won
   - **Win Delay**: Time in seconds before advancing

### 3.3 Add a Light Source
1. Create a 3D object: `GameObject > 3D Object > Cube`
2. Rename it to "LightSource"
3. Add the LightSource script: `Add Component > LightSource`
4. Set the tag to "LightSource" (or create the tag if needed)
5. Configure in Inspector:
   - **Emission Direction**: Direction the light travels (e.g., 0, 0, 1 for forward)
   - **Light Color**: Color of the beam (default yellow)
   - **Beam Width**: Thickness of the light beam
6. Position the light source in your scene

### 3.4 Add Mirrors
1. Create a Plane: `GameObject > 3D Object > Plane`
2. Rename it to "Mirror"
3. Scale it appropriately (e.g., 0.5, 0.5, 0.5 for a smaller mirror)
4. Add the Mirror script: `Add Component > Mirror`
5. Add a Box Collider: `Add Component > Box Collider`
6. Set the tag to "Mirror" (create if needed)
7. Configure in Inspector:
   - **Can Rotate**: Check to allow player rotation
   - **Rotation Speed**: How fast the mirror rotates (default 100)
   - **Lock X/Y/Z Rotation**: Choose which axes to lock
   - **Mirror Color**: Visual appearance color
8. Rotate the mirror to initial angle using the Transform tools
9. Duplicate mirrors as needed for your puzzle

### 3.5 Add Targets
1. Create a Sphere: `GameObject > 3D Object > Sphere`
2. Rename it to "Target"
3. Scale appropriately (e.g., 0.5, 0.5, 0.5)
4. Add the Target script: `Add Component > Target`
5. Add a Sphere Collider (should be added automatically)
6. Set the tag to "Target" (create if needed)
7. Configure in Inspector:
   - **Inactive Color**: Color when not hit by light (default red)
   - **Active Color**: Color when hit by light (default green)
   - **Glow Intensity**: Brightness multiplier when hit
8. Position the target where you want the light to reach
9. Add more targets for complex puzzles

### 3.6 Position the Camera
1. Select the Main Camera
2. Position it to see your entire level
3. Typical position: (0, 5, -10) looking at the center
4. Adjust the rotation to point at your game objects

## Step 4: Test Your Level

1. Click the Play button in Unity
2. Try clicking and dragging the mirrors to rotate them
3. Watch the light beam reflect off mirrors
4. Direct the light to hit all targets
5. When all targets are hit, you should see "Level Complete!" in the console

### Testing Tips:
- Check that mirrors have colliders and correct tags
- Verify targets have colliders and correct tags
- Ensure light source emission direction is set properly
- Use Scene view to debug positioning issues
- Check Console for any error messages

## Step 5: Design Tips

### Creating Good Puzzles:
1. **Start Simple**: Begin with one mirror and one target
2. **Add Complexity**: Gradually add more mirrors and targets
3. **Use Space**: Spread objects out to make beam paths clearer
4. **Angle Matters**: Pre-position mirrors at interesting angles
5. **Multiple Paths**: Consider if there are multiple solutions
6. **Test Often**: Play frequently to ensure it's solvable

### Visual Polish:
1. Adjust camera angle for best view
2. Add more lighting for ambiance
3. Use different colored materials for visual variety
4. Scale objects for better aesthetics
5. Add background elements (walls, floor) for context

## Step 6: Building Your Game

Once you have levels ready:

1. Go to `File > Build Settings`
2. Click "Add Open Scenes" to add your current scene
3. Drag scenes to reorder them (first scene loads first)
4. Choose your target platform
5. Click "Build" and choose output location

## Common Issues

### Light beam not appearing:
- Check that LightSource has the script attached
- Verify emission direction is not zero
- Ensure camera can see the light source position

### Mirror not rotating:
- Verify "Can Rotate" is checked in Mirror script
- Check that mirror has a Collider component
- Ensure mirror has "Mirror" tag

### Targets not registering hits:
- Confirm targets have colliders
- Check that tag is set to "Target"
- Verify light beam is actually reaching the target position

### Level not completing:
- Make sure GameManager exists in the scene
- Check that all targets are being hit simultaneously
- Look for error messages in the Console window

## Next Steps

- Create multiple levels with increasing difficulty
- Experiment with different mirror arrangements
- Add obstacles between mirrors
- Try different light source positions and angles
- Share your levels with others!

## Need Help?

If you encounter issues:
1. Check the Console window for error messages
2. Review the script files in `Assets/Scripts/`
3. Ensure all GameObjects have required components
4. Verify tags are correctly assigned
5. Check Unity's documentation for component details

Happy level designing!
