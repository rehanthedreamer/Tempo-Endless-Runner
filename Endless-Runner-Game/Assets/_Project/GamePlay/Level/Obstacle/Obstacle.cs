using UnityEngine;
using ERG;
public class Obstacle : MonoBehaviour
{
    [SerializeField] ObstacleType obstacleType;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public ObstacleType GetObstacleType()
    {
        return obstacleType;
    }
}

