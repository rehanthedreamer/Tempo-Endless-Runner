using System;
using TMPro;
using UnityEngine;

public class DistanceHUD : MonoBehaviour
{
    public static Action<float> OnDistanceHUDUpdate;

    [SerializeField] TMP_Text distanceText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        OnDistanceHUDUpdate += HUDDistanceUpdate;
    }

    void OnDisable()
    {
        OnDistanceHUDUpdate -= HUDDistanceUpdate;
    }
    void Start()
    {
    }

   void HUDDistanceUpdate(float distance)
    {
      
        distanceText.text = Extensions. DistanceFormat(distance);
        //SaveService.TrySetBestDistance(GameMode.Easy, distance);

    }
}
