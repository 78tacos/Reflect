# Prefabs Directory

This directory is for storing reusable game object prefabs.

## What are Prefabs?

Prefabs are pre-configured game objects that can be reused across multiple scenes. They're useful for:
- Creating consistent game objects
- Updating multiple instances at once
- Quickly building levels

## Suggested Prefabs to Create

### 1. Mirror Prefab
**Components:**
- Plane or Quad mesh
- Mirror.cs script
- Box Collider
- Material with semi-transparent appearance
- Tag: "Mirror"

**Default Settings:**
- Scale: 0.5, 0.05, 0.5
- Rotation: Can be adjusted per instance
- Color: Light blue with transparency

### 2. Light Source Prefab
**Components:**
- Cube or Sphere mesh
- LightSource.cs script
- Glowing material
- Tag: "LightSource"

**Default Settings:**
- Scale: 0.3, 0.3, 0.3
- Emission Direction: 0, 0, 1 (forward)
- Beam Color: Yellow
- Beam Width: 0.1

### 3. Target Prefab
**Components:**
- Sphere mesh
- Target.cs script
- Sphere Collider
- Material (will change color on hit)
- Tag: "Target"

**Default Settings:**
- Scale: 0.5, 0.5, 0.5
- Inactive Color: Red
- Active Color: Green
- Glow Intensity: 2

## How to Create a Prefab

1. Create and configure your GameObject in the scene
2. Drag it from the Hierarchy to this Assets/Prefabs folder in the Project window
3. Unity will create a prefab file
4. You can now drag instances of this prefab into any scene
5. Changes to the prefab will update all instances

## Using Prefabs in Your Levels

1. Drag the prefab from the Project window into the Scene or Hierarchy
2. Position and rotate as needed
3. Modify instance-specific properties in the Inspector
4. To apply changes back to the prefab, use "Overrides > Apply All"

## Benefits of Using Prefabs

- **Consistency**: All instances start with the same configuration
- **Efficiency**: Quickly populate levels with pre-configured objects
- **Maintainability**: Update all instances by editing the prefab
- **Organization**: Keep commonly used objects ready to use

## Tips

- Create variants of prefabs for different difficulty levels
- Name prefabs clearly (e.g., "Mirror_Small", "Mirror_Large")
- Test prefabs in a scene before using them widely
- Document any special configuration needs
