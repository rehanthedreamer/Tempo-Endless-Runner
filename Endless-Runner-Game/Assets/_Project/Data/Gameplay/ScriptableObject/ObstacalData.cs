using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObstacalData", menuName = "Scriptable Objects/ObstacalData")]
public class ObstacalData : ScriptableObject
{
    public List<Obstacle> obstacles = new List<Obstacle>();
}
