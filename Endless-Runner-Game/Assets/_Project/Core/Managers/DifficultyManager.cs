using UnityEngine;
using System;
using UnityEngine.UI;

public class DifficultyManager : Singleton<DifficultyManager>
{
    [SerializeField] private DifficultyConfig config;

    public static Action<float> OnSpeedMultiplierChanged;

    private float _timer;
    private float _currentMultiplier = 1f;
    [SerializeField] Slider difficultySlider;


    void Start()
    {
        OnSpeedMultiplierChanged?.Invoke(_currentMultiplier);
    }

    public float GetCurrentMultiplier()
    {
        return _currentMultiplier;
    }

    private void Update()
    {
       if( GameManager.Instance.CurrentState != GameState.inGame) return;
        _timer += Time.deltaTime;
        difficultySlider.value = Mathf.Clamp01(_timer / config.difficultyInterval);
        if (_timer >= config.difficultyInterval)
        {
            _timer = 0f;
            IncreaseDifficulty();
        }
    }

    /// <summary>
    /// brodcast once _currentMultiplier increased 
    /// </summary>

    private void IncreaseDifficulty()
    {
        _currentMultiplier += config.speedMultiplierStep;
        _currentMultiplier = Mathf.Min(_currentMultiplier, config.maxMultiplier);
        OnSpeedMultiplierChanged?.Invoke(_currentMultiplier);
    }

/// <summary>
/// reset 
/// </summary>
    public void ResetDifficulty()
    {
        _currentMultiplier = 1;
         _timer = 0;
        OnSpeedMultiplierChanged?.Invoke(_currentMultiplier);
    }

     public void StopDifficulty()
    {
        _currentMultiplier = 0f;
        OnSpeedMultiplierChanged?.Invoke(_currentMultiplier);
    }
}
