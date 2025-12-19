using System;
using TMPro;
using UnityEngine;

public class CoinHUD : MonoBehaviour
{
    public static Action<int> OnCoinUICoinUpdate;

    [SerializeField] TMP_Text coinText;
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

   void HUDCoinUpdate(int coinAmt)
    {
        SaveService.AddCoins(coinAmt);
        coinText.text = SaveService.GetCoins().ToString();
    }
}
