# Contributing to Reflect

Thank you for your interest in contributing to the Reflect game project! This document provides guidelines for contributing to the project.

## Ways to Contribute

- **Report Bugs**: Found a bug? Open an issue with details
- **Suggest Features**: Have ideas for new features? Share them!
- **Create Levels**: Design and share new puzzle levels
- **Improve Code**: Submit pull requests with enhancements
- **Write Documentation**: Help improve guides and docs
- **Share Materials**: Create and share visual assets

## Getting Started

1. Fork the repository
2. Clone your fork locally
3. Create a new branch for your feature/fix
4. Make your changes
5. Test thoroughly
6. Submit a pull request

## Code Style Guidelines

### C# Conventions
- Use PascalCase for class names and public methods
- Use camelCase for private fields and local variables
- Prefix private fields with underscore (optional but consistent)
- Add XML documentation comments for public APIs
- Keep methods focused and single-purpose

### Example:
```csharp
/// <summary>
/// Calculates the reflection angle of the light beam
/// </summary>
/// <param name="incomingDirection">Direction of incoming light</param>
/// <param name="surfaceNormal">Normal of the reflective surface</param>
/// <returns>Reflected direction vector</returns>
public Vector3 CalculateReflection(Vector3 incomingDirection, Vector3 surfaceNormal)
{
    return Vector3.Reflect(incomingDirection, surfaceNormal);
}
```

## Unity Project Guidelines

### Scene Organization
- Keep scenes clean and organized
- Use empty GameObjects as folders in hierarchy
- Name objects descriptively
- Group related objects under parent objects

### Script Organization
- One script per file
- Place scripts in appropriate folders
- Keep scripts focused on single responsibility
- Use [SerializeField] for inspector variables
- Use [Header] attributes to organize inspector sections

### Prefab Usage
- Create prefabs for reusable objects
- Update prefabs rather than individual instances when possible
- Use prefab variants for specialized versions

## Feature Development

### Adding New Mechanics

When adding new game mechanics:

1. **Plan First**: Outline the feature and its impact
2. **Create Script**: Write clean, documented code
3. **Test Thoroughly**: Ensure it works in various scenarios
4. **Update Documentation**: Add to README or create new guide
5. **Create Example**: Make a scene demonstrating the feature

### Example Features to Add
- Color-coded lights and targets
- Prisms that split light beams
- Movable mirrors
- Timed challenges
- Score tracking
- Sound effects
- Particle effects
- Level editor UI

## Level Design Contributions

### Creating Good Levels

When designing levels:

1. **Start Simple**: Gradually introduce complexity
2. **Test Thoroughly**: Ensure the level is solvable
3. **Clear Goals**: Make objectives obvious
4. **Fair Difficulty**: Challenge without frustration
5. **Visual Clarity**: Keep the layout clean and readable

### Level Submission Format

When submitting a level:
- Save scene in `Assets/Scenes/Community/`
- Name it clearly (e.g., `Community_YourName_01.unity`)
- Include a comment in the scene with:
  - Your name
  - Brief description
  - Difficulty rating (Easy/Medium/Hard)
  - Estimated completion time

## Testing Guidelines

Before submitting changes:

### Functional Testing
- [ ] Feature works as intended
- [ ] No console errors or warnings
- [ ] Compatible with existing features
- [ ] Doesn't break existing levels

### Performance Testing
- [ ] No significant frame rate drops
- [ ] Memory usage is reasonable
- [ ] No infinite loops or hangs

### Compatibility Testing
- [ ] Works in Unity Editor
- [ ] Builds successfully
- [ ] Compatible with target platforms

## Pull Request Process

1. **Update Documentation**: Include any necessary doc changes
2. **Follow Style Guide**: Ensure code follows project conventions
3. **Write Clear Commit Messages**: Describe what and why
4. **Keep Changes Focused**: One feature/fix per PR
5. **Test Thoroughly**: Verify everything works
6. **Describe Changes**: Provide clear PR description

### PR Description Template
```markdown
## Changes
Brief description of what changed

## Motivation
Why this change is needed

## Testing
How you tested the changes

## Screenshots
(If applicable) Visual changes

## Checklist
- [ ] Code follows style guidelines
- [ ] Tested in Unity Editor
- [ ] Documentation updated
- [ ] No new warnings or errors
```

## Bug Reports

When reporting bugs, please include:

- **Unity Version**: Which version you're using
- **Platform**: Windows, Mac, Linux, etc.
- **Steps to Reproduce**: Detailed steps to recreate the bug
- **Expected Behavior**: What should happen
- **Actual Behavior**: What actually happens
- **Screenshots/Logs**: If applicable
- **Scene/Level**: Which scene the bug occurs in

## Feature Requests

When suggesting features:

- **Clear Description**: What the feature does
- **Use Case**: Why it's useful
- **Implementation Ideas**: How it might work (optional)
- **Mockups**: Visual examples if applicable (optional)

## Asset Contributions

### Materials and Textures
- Use appropriate file formats (PNG, JPG for textures)
- Keep file sizes reasonable
- Include source files if applicable
- Provide attribution if using external assets

### Audio
- Use common formats (WAV, OGG, MP3)
- Ensure proper licensing
- Include attribution for external assets

### Models
- Use FBX or OBJ format
- Keep polygon count reasonable
- Include proper materials and textures
- Provide attribution for external assets

## Code of Conduct

- Be respectful and professional
- Welcome newcomers and help them learn
- Give constructive feedback
- Focus on the work, not the person
- Assume good intentions

## Questions?

If you have questions about contributing:
- Open an issue for discussion
- Check existing issues and documentation
- Reach out to project maintainers

## License

By contributing, you agree that your contributions will be licensed under the MIT License that covers the project.

---

Thank you for contributing to Reflect! Your efforts help make this project better for everyone.
