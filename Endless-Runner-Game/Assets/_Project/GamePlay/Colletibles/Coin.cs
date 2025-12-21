using UnityEngine;

public class Coin : PoolableObject
{
    public override void OnSpawned()
    {
        base.OnSpawned();
    }

    public override void OnDespawned()
    {
        base.OnDespawned();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            CoinHUD.OnCoinUICoinUpdate?.Invoke(SaveService.GetCoinMultiplierPower()? 2:1);
            SoundManager.Instance.PlaySFX(SoundManager.Instance.coin);
            OnReleaseRequest();
        }

    }
}
