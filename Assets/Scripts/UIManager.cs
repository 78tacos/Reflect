using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages UI elements and display updates
/// </summary>
public class UIManager : MonoBehaviour
{
    [Header("UI Text References")]
    [SerializeField] private Text targetCountText;
    [SerializeField] private Text instructionText;
    
    private GameManager gameManager;
    
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        
        if (instructionText != null)
        {
            instructionText.text = "Click and drag mirrors to reflect light to the targets!\nR - Restart | N - Next Level";
        }
    }
    
    void Update()
    {
        UpdateTargetCount();
    }
    
    private void UpdateTargetCount()
    {
        if (targetCountText != null && gameManager != null)
        {
            int total = gameManager.GetTargetCount();
            int hit = gameManager.GetHitTargetCount();
            targetCountText.text = $"Targets: {hit}/{total}";
        }
    }
}
