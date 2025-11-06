# Materials Directory

This directory is for storing Unity materials used for game object appearances.

## Suggested Materials to Create

### 1. Mirror Material
**Properties:**
- **Shader**: Standard or Unlit
- **Rendering Mode**: Transparent or Fade
- **Albedo Color**: Light cyan/blue (e.g., RGB: 179, 230, 255)
- **Metallic**: 0.8-1.0 (high reflectivity)
- **Smoothness**: 0.9-1.0 (very smooth)
- **Alpha**: 0.5-0.7 (semi-transparent)

**Usage**: Apply to mirror planes for a reflective, glass-like appearance

### 2. Light Source Material
**Properties:**
- **Shader**: Standard with Emission
- **Albedo Color**: Yellow (e.g., RGB: 255, 255, 0)
- **Emission**: Enabled
- **Emission Color**: Bright yellow (same as albedo)
- **Emission Intensity**: 2-3

**Usage**: Apply to light source objects to make them glow

### 3. Target Inactive Material
**Properties:**
- **Shader**: Standard with Emission
- **Albedo Color**: Red (e.g., RGB: 255, 0, 0)
- **Emission**: Enabled
- **Emission Color**: Dark red
- **Emission Intensity**: 0.5

**Usage**: Applied to targets when not hit by light (handled by Target.cs script)

### 4. Target Active Material
**Properties:**
- **Shader**: Standard with Emission
- **Albedo Color**: Green (e.g., RGB: 0, 255, 0)
- **Emission**: Enabled
- **Emission Color**: Bright green
- **Emission Intensity**: 2

**Usage**: Applied to targets when hit by light (handled by Target.cs script)

### 5. Level Background Material
**Properties:**
- **Shader**: Standard
- **Albedo Color**: Dark gray or dark blue
- **Metallic**: 0
- **Smoothness**: 0.3

**Usage**: Apply to floor/background planes for a clean game area

## How to Create a Material in Unity

1. Right-click in this folder (Project window)
2. Select `Create > Material`
3. Name the material appropriately
4. Select the material to edit it in the Inspector
5. Choose a shader (Standard is most versatile)
6. Adjust color, metallic, smoothness, and other properties
7. Enable emission if you want glowing effects

## Applying Materials

1. Select a GameObject in the scene or Hierarchy
2. In the Inspector, find the Mesh Renderer component
3. Expand the Materials section
4. Drag your material onto the Element 0 slot
5. Or click the circle icon and select from the list

## Material Tips

- **Test in Play Mode**: See how materials look with lighting
- **Use Emission Sparingly**: Too much glow can be distracting
- **Consistent Style**: Keep similar objects using similar material styles
- **Performance**: Standard shader is optimized for most uses
- **Transparency**: Use for special effects, but can impact performance

## Color Suggestions

For a cohesive look, consider this color palette:

- **Mirrors**: Light blue/cyan with transparency
- **Light Beams**: Bright yellow or white
- **Targets (inactive)**: Red with slight glow
- **Targets (active)**: Bright green with strong glow
- **Background**: Dark blue or gray
- **UI Elements**: White or light colors for contrast

## Advanced Materials

For more advanced visuals, you can:
- Use custom shaders
- Add normal maps for surface detail
- Apply textures for variety
- Create animated materials with scripts
- Use HDR colors for bloom effects

## Note

The Target.cs and other scripts may dynamically modify material properties at runtime for visual feedback. Materials listed here are starting points that can be enhanced based on your artistic direction.
