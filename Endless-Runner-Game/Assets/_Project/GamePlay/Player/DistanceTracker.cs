using UnityEngine;

public class DistanceTracker : MonoBehaviour
{
     [SerializeField] private float moveSpeed = 5f;

    private float _distance;

    public float Distance => _distance;

    private void Update()
    {
        _distance += moveSpeed * Time.deltaTime;
        DistanceHUD.OnDistanceHUDUpdate.Invoke(_distance);
    }

    public void ResetDistance()
    {
        _distance = 0f;
        DistanceHUD.OnDistanceHUDUpdate.Invoke(_distance);
    }
}
