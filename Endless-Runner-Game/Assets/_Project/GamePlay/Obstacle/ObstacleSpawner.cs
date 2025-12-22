using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : Singleton<ObstacleSpawner>
{
    [Header("Platform DataSO")]
    [SerializeField] ObstacalData obstacalData;
    List<PoolableObject> obstacalQueue = new List<PoolableObject>(); 
    // queue test
     //Queue<PoolableObject> obstacalQueue = new Queue<PoolableObject>(); 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreatePool();
        obstacalQueue.Shuffle();

    }
/// <summary>
/// Create pool
/// </summary>
    void CreatePool()
    {
        for (int i = 0; i < obstacalData.obstacles.Count; i++)
        {
            for (int j = 0; j < GameConstants.POOL_INITIAL_SIZE; j++)
            {
                PoolableObject p = Instantiate(obstacalData.obstacles[i].gameObject).GetComponent<PoolableObject>();
                p.OnReleaseRequested += ReturnToPool;
                p.OnDespawned();
                p.transform.parent = transform;
                obstacalQueue.Add(p);
                //obstacalQueue.Enqueue(p);
            }
        }
       
    }
/// <summary>
/// spawn from pool
/// </summary>
/// <param name="boxCollider2D"></param>
     public void SpawnObstacle(BoxCollider2D boxCollider2D)
    {
        int spawnProb = (int)Random.Range(1, 10);
        if(spawnProb >obstacalData.spawnProbability) return;
        if (obstacalQueue.Count == 0)
        {
            PoolableObject p = Instantiate(obstacalData.obstacles[Random.Range(0, obstacalData.obstacles.Count)].gameObject).GetComponent<PoolableObject>();
            p.OnReleaseRequested += ReturnToPool;
            p.OnDespawned();
            p.transform.parent = transform;
            obstacalQueue.Add(p);
            //obstacalQueue.Enqueue(p);
        }
        Vector3 position =  GetPointAboveCollider(boxCollider2D); 
        //PoolableObject obj = obstacalQueue.Dequeue();
        PoolableObject obj = obstacalQueue.Find(o => !o.gameObject.activeInHierarchy);
        obj.transform.parent = boxCollider2D.transform;
        obj.transform.SetPositionAndRotation(position, Quaternion.identity);
        obj.OnSpawned();
       
    }
/// <summary>
/// spawn position on the platform
/// </summary>
/// <param name="box"></param>
/// <returns></returns>
    private Vector3 GetPointAboveCollider(BoxCollider2D box)
    {
        Bounds bounds = box.bounds;

        float x = Random.Range(bounds.min.x+1.5f, bounds.max.x-1.5f);
        float y = box.transform.position.y + obstacalData.oYOffset; // above platform

        return new Vector3(x, y, 0f);
    }

/// <summary>
/// return to pool system
/// </summary>
/// <param name="poolable"></param>

   void ReturnToPool(IPoolable poolable)
    {
         PoolableObject obj = poolable as PoolableObject;
        obj.OnDespawned();
       // obstacalQueue.Enqueue(obj);
    }
    public void ReturnAllSpawnedToPool()
    {
        foreach (PoolableObject obj in obstacalQueue)
        {
            if (obj.gameObject.activeInHierarchy)
            {
                obj.OnReleaseRequest();
            }
        }
    }
  
}
