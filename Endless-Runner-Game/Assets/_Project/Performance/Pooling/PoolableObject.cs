using System;
using UnityEngine;

public abstract class PoolableObject : MonoBehaviour, IPoolable
{
    public event Action<IPoolable> OnReleaseRequested;

      public virtual void OnDespawned()
    {
        gameObject.SetActive(false);
    }

    public virtual void OnSpawned()
    {
        gameObject.SetActive(true);
    }

    internal protected void OnReleaseRequest()
    {
       OnReleaseRequested?.Invoke(this);
       if(OnReleaseRequested == null)
       OnDespawned();
    }
    
}
