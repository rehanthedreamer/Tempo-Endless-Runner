using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "StoreDataConfig", menuName = "Scriptable Objects/StoreDataConfig")]
public class StoreDataConfig : ScriptableObject
{
public List<StoreItem> storeItemList = new List<StoreItem>();
public StoreItemUI storeItemUIPrefab;
    
}

[Serializable]
public class StoreItem
{
    public PowerUps powerUps;
    public string discription;
    public float buyValue;
    public Sprite icon;
    public bool isPurchased = false;
}
