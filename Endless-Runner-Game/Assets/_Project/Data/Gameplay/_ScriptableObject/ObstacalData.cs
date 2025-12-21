using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObstacalData", menuName = "Scriptable Objects/ObstacalData")]
public class ObstacalData : ScriptableObject
{
    public List<Obstacle> obstacles = new List<Obstacle>();

    [Header("Obstacle Spawn Y Offset")]
    public float oYOffset =.8f;

    [Header("Obstacle Spawn Probability")]
    [Range(1, 5)]
    public int spawnProbability;
  
}
