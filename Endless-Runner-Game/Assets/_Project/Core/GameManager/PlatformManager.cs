using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatformManager : Singleton<PlatformManager>
{
    [Header("Platform DataSO")]
    [SerializeField] PlatformData platformData;
    Queue<PoolableObject> platformMoverQueue = new Queue<PoolableObject>(); 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreatePool();
        platformMoverQueue.Shuffle();
    }

    void CreatePool()
    {
        for (int i = 0; i < platformData.platforms.Count; i++)
        {
            for (int j = 0; j < GameConstants.POOL_INITIAL_SIZE; j++)
            {
                PoolableObject p = Instantiate(platformData.platforms[i].gameObject).GetComponent<PoolableObject>();
                p.OnReleaseRequested += ReturnToPool;
                p.OnDespawned();
                p.transform.parent = transform;
                platformMoverQueue.Enqueue(p);
            }
        }
       
    }

     public PoolableObject SpawnPlatform(Vector3 position)
    {
        if (platformMoverQueue.Count == 0)
        {
            PoolableObject p = Instantiate(platformData.platforms[Random.Range(0, platformData.platforms.Count)].gameObject).GetComponent<PoolableObject>();
            p.OnReleaseRequested += ReturnToPool;
            p.OnDespawned();
            p.transform.parent = transform;
            platformMoverQueue.Enqueue(p);
        }

        PoolableObject obj = platformMoverQueue.Dequeue();
        Vector3 newPos = position;
        newPos.x +=Random.Range(platformData.pXMinOffset, platformData.pXMaxOffset);
        newPos.y = Random.Range(platformData.pYMinOffset, platformData.pYMaxOffset);
        obj.transform.SetPositionAndRotation(newPos, Quaternion.identity);
        obj.GetComponent<PlatformMover>().SetSpeed(platformData.initialMoveSpeed);
        obj.OnSpawned();
        // spawn coin on platform
        CoinSpawner.Instance.SpawnCoins(obj.GetComponent<BoxCollider2D>());
        return obj;
    }

   void ReturnToPool(IPoolable poolable)
    {
         PoolableObject obj = poolable as PoolableObject;
        obj.OnDespawned();
        platformMoverQueue.Enqueue(obj);
    }

    private void OnDestroy()
    {
        // Clean up subscriptions
        foreach (var obj in platformMoverQueue)
        {
            obj.OnReleaseRequested -= ReturnToPool;
        }
    }

}
