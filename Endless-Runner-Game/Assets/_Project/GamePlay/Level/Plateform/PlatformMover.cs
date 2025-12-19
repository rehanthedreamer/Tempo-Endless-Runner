using UnityEngine;

public class PlatformMover : PoolableObject
{
   [SerializeField] float baseSpeed = 5f;
    float _currentSpeed;

    private void OnEnable()
    {
        DifficultyManager.OnSpeedMultiplierChanged += UpdateSpeed;
    }

    private void OnDisable()
    {
        DifficultyManager.OnSpeedMultiplierChanged -= UpdateSpeed;
    }

    public void SetSpeed(float baseSpeed)
    {
        this.baseSpeed = baseSpeed;
         _currentSpeed = baseSpeed * DifficultyManager.Instance.GetCurrentMultiplier();
    }
    
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector3.left * _currentSpeed * Time.deltaTime);
    }

    public override void OnSpawned()
    {
        base.OnSpawned();
    }

    public override void OnDespawned()
    {
        base.OnDespawned();
    }
     private void UpdateSpeed(float multiplier)
    {
        _currentSpeed = baseSpeed * multiplier;
    }
 
}
