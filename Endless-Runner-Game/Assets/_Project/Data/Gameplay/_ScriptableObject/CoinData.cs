using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CoinData", menuName = "Scriptable Objects/CoinData")]
public class CoinData : ScriptableObject
{
    [Header("Platform Type")]
    public GameObject coinPrefab;
    [Header("Coin Spawn Y Offset")]
    public float cYOffset =.8f;
    [Header("Coin Spawn x Range")]
    public float cXOffset =.8f;
}
