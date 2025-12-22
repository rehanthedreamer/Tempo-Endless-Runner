using UnityEngine;

public class DistanceTracker : MonoBehaviour
{
     [SerializeField] private float moveSpeed = 5f;
     float _currentSpeed;

    private float _distance;

    public float Distance => _distance;

      private void OnEnable()
    {
        DifficultyManager.OnSpeedMultiplierChanged += IncreaseSpeed;
    }

    private void OnDisable()
    {
        DifficultyManager.OnSpeedMultiplierChanged -= IncreaseSpeed;
    }

    public void IncreaseSpeed(float multiplier)
    {
     
        _currentSpeed = moveSpeed * multiplier;
        
    }
/// <summary>
/// calculate player covered distance 
/// </summary>
    private void Update()
    {
        if(GameManager.Instance.CurrentState != GameState.inGame)return;
        _distance += _currentSpeed * Time.deltaTime;
        DistanceHUD.OnDistanceHUDUpdate.Invoke(_distance);
    }

    public void ResetDistance()
    {
        _distance = 0f;
        DistanceHUD.OnDistanceHUDUpdate.Invoke(_distance);
    }
}
