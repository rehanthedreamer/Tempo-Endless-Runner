using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class CoinSpawner : Singleton<CoinSpawner>
{
    [Header("Platform DataSO")]
    [SerializeField] CoinData coinData;
     List<PoolableObject> CoinQueue = new List<PoolableObject>(); 
     // Queue test
   // Queue<PoolableObject> CoinQueue = new Queue<PoolableObject>(); 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreatePool();
    }

    void CreatePool()
    {
            for (int j = 0; j < GameConstants.POOL_INITIAL_SIZE+12; j++)
            {
                PoolableObject p = Instantiate(coinData.coinPrefab).GetComponent<PoolableObject>();
                p.OnReleaseRequested += ReturnToPool;
                p.OnDespawned();
                p.transform.parent = transform;
                CoinQueue.Add(p);
               // CoinQueue.Enqueue(p);
            }
       
    }

     public void SpawnCoins( BoxCollider2D boxCollider2D)
    {
       
        int coinCount = Random.Range(0, 4);
        CheckPoolSize(coinCount);
        Vector3 position =  GetPointAboveCollider(boxCollider2D); 

        for (int i = 0; i < coinCount; i++)
        {
          //  PoolableObject obj = CoinQueue.Dequeue();
            PoolableObject obj = CoinQueue.Find(c => !c.gameObject.activeInHierarchy);
            Vector3 newPos = position;
            newPos.x +=coinData.cXOffset*i;
            obj.transform.parent = boxCollider2D.transform;
            obj.transform.SetPositionAndRotation(newPos, Quaternion.identity);
            obj.OnSpawned();
        }        
    }

    private void CheckPoolSize(int required)
    {
        while (CoinQueue.Count < required)
        {
            PoolableObject p = Instantiate(coinData.coinPrefab, transform)
                                .GetComponent<PoolableObject>();

            p.OnReleaseRequested += ReturnToPool;
            p.OnDespawned();
            CoinQueue.Add(p);
            //CoinQueue.Enqueue(p);
        }
    }

    private Vector3 GetPointAboveCollider(BoxCollider2D box)
    {
        Bounds bounds = box.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = box.transform.position.y + coinData.cYOffset; // above platform

        return new Vector3(x, y, 0f);
    }


   void ReturnToPool(IPoolable poolable)
    {
        PoolableObject obj = poolable as PoolableObject;
        obj.OnDespawned();
        //CoinQueue.Enqueue(obj);
    }
    public void ReturnAllSpawnedToPool()
    {
       // List<PoolableObject> temp = new List<PoolableObject>(spawnedObjects);
        foreach (PoolableObject obj in CoinQueue)
        {
            if (obj.gameObject.activeInHierarchy)
            {
                obj.OnReleaseRequest();
            }
        }
    }

   
}
