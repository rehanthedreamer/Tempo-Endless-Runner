using UnityEngine;

[CreateAssetMenu(fileName = "DifficultyConfig", menuName = "Scriptable Objects/DifficultyConfig")]
public class DifficultyConfig : ScriptableObject
{
    public float difficultyInterval = 10f;   // seconds
    public float speedMultiplierStep = 0.1f; // +10% each step
    public float maxMultiplier = 3f;

    void OnEnable()
    {
         GameManager.OnGameModeChanged += ApplyGameModeConfig;
    }

    void OnDisable()
    {
        GameManager.OnGameModeChanged -= ApplyGameModeConfig;
    }

    void ApplyGameModeConfig(GameModeConfig gameModeConfig)
    {
        difficultyInterval = gameModeConfig.difficultyInterval;
        speedMultiplierStep = gameModeConfig.difficultyMultiplierStep;
        
    }

}
