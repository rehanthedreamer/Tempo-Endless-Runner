using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : Singleton<CoinSpawner>
{
    [Header("Platform DataSO")]
    [SerializeField] CoinData coinData;
    Queue<PoolableObject> CoinQueue = new Queue<PoolableObject>(); 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreatePool();
    }

    void CreatePool()
    {
            for (int j = 0; j < GameConstants.POOL_INITIAL_SIZE+5; j++)
            {
                PoolableObject p = Instantiate(coinData.coinPrefab).GetComponent<PoolableObject>();
                p.OnReleaseRequested += ReturnToPool;
                p.OnDespawned();
                p.transform.parent = transform;
                CoinQueue.Enqueue(p);
            }
       
    }

     public void SpawnCoins( BoxCollider2D boxCollider2D)
    {
       
        int coinCount = Random.Range(0, 5);
        CheckPoolSize(coinCount);
        Vector3 position =  GetPointAboveCollider(boxCollider2D); 

        for (int i = 0; i < coinCount; i++)
        {
            PoolableObject obj = CoinQueue.Dequeue();
           
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
            CoinQueue.Enqueue(p);
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
        CoinQueue.Enqueue(obj);
        StartCoroutine(ReparentNextFrame(obj));
    }
    private IEnumerator ReparentNextFrame(PoolableObject obj)
{
    yield return null; // Wait one frame

    if (obj != null && obj.gameObject != null)
    {
        obj.transform.SetParent(transform, false);
    }
}

    private void OnDestroy()
    {
        // Clean up subscriptions
        foreach (var obj in CoinQueue)
        {
            obj.OnReleaseRequested -= ReturnToPool;
        }
    }
}
