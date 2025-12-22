using System.Collections;
using UnityEngine;

public class ParallaxManager : MonoBehaviour
{
   [SerializeField] Parallax[] parallaxes;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

      private void OnEnable()
    {
        DifficultyManager.OnSpeedMultiplierChanged += IncreaseParallaxSpeed;
    }

    private void OnDisable()
    {
        DifficultyManager.OnSpeedMultiplierChanged -= IncreaseParallaxSpeed;
    }

/// <summary>
/// increase Parallax speed over the time
/// </summary>
/// <param name="multiplier"></param>
    public void IncreaseParallaxSpeed(float multiplier)
    {
        foreach (var p in parallaxes)
        {
            p._currentSpeed = p.baseSpeed * multiplier;
        }
    }
}
