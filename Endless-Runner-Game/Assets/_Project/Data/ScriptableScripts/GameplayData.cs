using UnityEngine;
using System;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "GameplayData", menuName = "Scriptable Objects/GameplayData")]
public class GameplayData : ScriptableObject
{
    public List<GameModeConfig> gameModeConfigs = new List<GameModeConfig>();

    public GameModeConfig GetGameModeData(GameMode gameMode)
    {
        return gameModeConfigs.Find(d => d.gameMode == gameMode);
    }
}

[Serializable]
public class GameModeConfig
{
   public GameMode gameMode;
    [Header("Game Difficulty")]
    public float currentMultiplier;
    public float difficultyInterval;
    public float difficultyMultiplierStep;

    [Header("Game Obstacle Probability")]
    public int obstacleSpawnProbability;

    [Header("Game Platform x gap")]
    public float pXMinOffset;
    public float pXMaxOffset;
     
}
