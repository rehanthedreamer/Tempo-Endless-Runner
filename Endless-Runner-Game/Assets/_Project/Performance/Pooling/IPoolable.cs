using System;
using UnityEngine;

public interface IPoolable 
{
    event Action<IPoolable> OnReleaseRequested;
    void OnSpawned();
    void OnDespawned();
}
