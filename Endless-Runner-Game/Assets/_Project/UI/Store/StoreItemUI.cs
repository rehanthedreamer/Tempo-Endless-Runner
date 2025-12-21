using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreItemUI : MonoBehaviour
{
    StoreItem storeItem;
    [SerializeField] Image itemIcon;
    [SerializeField] TMP_Text itemDescription;
    [SerializeField] TMP_Text itemBuyValue;
    [SerializeField] Button itemBuyBtn;

    void OnEnable()
    {
        itemBuyBtn.onClick.AddListener(OnClickBuyItem);
        CoinHUD.OnCoinUICoinUpdate += CheckCoinToBuy;
    }

    void OnDisable()
    {
        itemBuyBtn.onClick.RemoveListener(OnClickBuyItem);
        CoinHUD.OnCoinUICoinUpdate -= CheckCoinToBuy;
    }
 
    public void IntitStoreItem(StoreItem storeItem)
    {
        this.storeItem = storeItem;
        itemIcon.sprite =  this.storeItem.icon;
        itemDescription.text = this.storeItem.discription;
        itemBuyValue.text = "$"+this.storeItem.buyValue.ToString();
    }

   void CheckCoinToBuy(int coin)
    {
       itemBuyBtn.interactable = SaveService.GetCoins() > storeItem.buyValue && !storeItem.isPurchased ? true : false;
    }

    void OnClickBuyItem()
    {
        if(SaveService.GetCoins() > storeItem.buyValue)
        {
            CoinHUD.OnCoinUICoinUpdate(-(int)storeItem.buyValue);
            storeItem.isPurchased = true;
        }
     
    }
}
