using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlatformData", menuName = "Scriptable Objects/PlatformData")]
public class PlatformData : ScriptableObject
{
    [Header("Platform Type")]
    public List<GameObject> platforms = new List<GameObject>();
    [Header("Platform Speed")]
    public float initialMoveSpeed =2;
    [Header("Platform Spawn Y Range")]
    public float pYMinOffset =0;
    public float pYMaxOffset =2;
    [Header("Platform Spawn x Range")]
    public float pXMinOffset =0;
    public float pXMaxOffset = 3.5f;
   
}
