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
        CoinHUD.OnCoinUICoinUpdate += ChecklItemAvailablity;
        
    }

    void OnDisable()
    {
        itemBuyBtn.onClick.RemoveListener(OnClickBuyItem);
        CoinHUD.OnCoinUICoinUpdate -= ChecklItemAvailablity;
    }
 
    public void IntitStoreItem(StoreItem storeItem)
    {
        this.storeItem = storeItem;
        itemIcon.sprite =  this.storeItem.icon;
        itemDescription.text = this.storeItem.discription;
        itemBuyValue.text = this.storeItem.buyValue.ToString();
        ChecklItemAvailablity(0);
    }

   void ChecklItemAvailablity(int coin)
    {
       itemBuyBtn.interactable = SaveService.GetCoins() > storeItem.buyValue && !storeItem.isPurchased ? true : false;
    }

    void OnClickBuyItem()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.btnClickSound);
        if(SaveService.GetCoins() > storeItem.buyValue)
        {
            CoinHUD.OnCoinUICoinUpdate(-(int)storeItem.buyValue);
            storeItem.isPurchased = true;
            ChecklItemAvailablity(0);
            HandlePowerUpActiveStatus();
        }
     
    }

    void HandlePowerUpActiveStatus()
    {
        if(storeItem.powerUps == PowerUps.DubbleDash)
         SaveService.SetDubbleJumpPower(storeItem.isPurchased);
        if(storeItem.powerUps == PowerUps.Shield)
         SaveService.SetShieldPower(storeItem.isPurchased);
        if(storeItem.powerUps == PowerUps.CoinMultiplayer)
         SaveService.SetCoinMultiplierPower(storeItem.isPurchased);
    }
}
