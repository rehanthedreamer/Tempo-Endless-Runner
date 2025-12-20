using UnityEngine;
using ERG;
public class Obstacle : PoolableObject
{
    [SerializeField] ObstacleType obstacleType;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public ObstacleType GetObstacleType()
    {
        return obstacleType;
    }

    public override void OnSpawned()
    {
        base.OnSpawned();
    }

    public override void OnDespawned()
    {
        base.OnDespawned();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            if(obstacleType == ObstacleType.Box)
            GetComponent<Rigidbody2D>().AddForce(transform.forward*5);
            OnReleaseRequest();
        }
     
    }


}

