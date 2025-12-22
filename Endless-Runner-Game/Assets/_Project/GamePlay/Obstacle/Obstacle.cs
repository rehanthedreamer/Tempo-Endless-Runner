using UnityEngine;
using System.Collections;
public class Obstacle : PoolableObject
{
    [SerializeField] ObstacleType obstacleType;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    BoxCollider2D boxCollider2D;
    void Awake()
    {
      boxCollider2D =  GetComponent<BoxCollider2D>();
    }
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
        boxCollider2D.isTrigger = false;
    }

     void OnDisable()
    {
         OnReleaseRequest();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            if(obstacleType == ObstacleType.Box)
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.right * 10f, ForceMode2D.Impulse);
                boxCollider2D.isTrigger = true;
                StartCoroutine(DeSpawnAfterDelay());
            }else
                OnReleaseRequest();
        }
     
    }

    IEnumerator DeSpawnAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        OnReleaseRequest();
    }


}

