using UnityEngine;

public class StoreManager : Singleton<StoreManager>
{
    [SerializeField] StoreDataConfig storeDataConfig;
    [SerializeField] Transform itemContainer;

    void Start()
    {
        InitStoreItems();
    }
    public void InitStoreItems()
    {
        for (int i = 0; i < storeDataConfig.storeItemList.Count; i++)
        {
            var storeItem = Instantiate(storeDataConfig.storeItemUIPrefab).GetComponent<StoreItemUI>();
            storeItem.transform.parent = itemContainer;
            storeItem.IntitStoreItem(storeDataConfig.storeItemList[i]);
            storeItem.GetComponent<RectTransform>().localScale = Vector3.one;
        }
    }
    
}
