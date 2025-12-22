using System;
using TMPro;
using UnityEngine;

public class CoinHUD : MonoBehaviour
{
    public static Action<int> OnCoinUICoinUpdate;

    [SerializeField] TMP_Text coinText;
    [SerializeField] TMP_Text coinTextOnSetting;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        OnCoinUICoinUpdate += HUDCoinUpdate;
    }

    void OnDisable()
    {
        OnCoinUICoinUpdate -= HUDCoinUpdate;
    }
    void Start()
    {
       HUDCoinUpdate(0);
    }
/// <summary>
/// coin update and save once broadcasted 
/// </summary>
/// <param name="coinAmt"></param>
   void HUDCoinUpdate(int coinAmt)
    {
        SaveService.AddCoins(coinAmt);
        coinText.text = SaveService.GetCoins().ToString();
        coinTextOnSetting.text =SaveService.GetCoins().ToString();
    }
}
