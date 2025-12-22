using System;
using System.Collections.Generic;
using UnityEngine;

public static class SaveService
{

    // --------------------
    // DATA MODEL
    // --------------------

    [Serializable]
    private class PlayerSaveData
    {
        public float bestDistance;
        public int coin;
        public bool isDubbleJump = false;
        public bool isShield = false;
        public bool coinMultiplier = false;
    }

    // --------------------
    // Internal Access
    // --------------------

    private static string GetKey()
    {
        return GameConstants.SAVE_KEY_PREFIX;
    }

    private static PlayerSaveData LoadInternal()
    {
        string key = GetKey();

        if (PlayerPrefs.HasKey(key))
        {
            string json = PlayerPrefs.GetString(key);
            return JsonUtility.FromJson<PlayerSaveData>(json);
        }
        return new PlayerSaveData();
    }

    private static void SaveInternal( PlayerSaveData data)
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(GetKey(), json);
        PlayerPrefs.Save();
    }

    // --------------------
    // PUBLIC Access
    // --------------------

    public static int GetCoins()
    {
        return LoadInternal().coin; 
    }

    public static void AddCoins( int amount)
    {
        var data = LoadInternal();
        data.coin += amount;
        SaveInternal( data);
    }

    public static float GetBestDistance()
    {
        return LoadInternal().bestDistance;
    }

    public static void TrySetBestDistance( float distance)
    {
        var data = LoadInternal();

        if (distance > data.bestDistance)
        {
            data.bestDistance = distance;
            SaveInternal( data);
        }
    }

/// <summary>
/// Power ups data
/// </summary>
/// <returns></returns>
    public static bool GetDubbleJumpPower()
    {
        return LoadInternal().isDubbleJump;
    }
      public static bool GetShieldPower()
    {
        return LoadInternal().isShield;
    }
      public static bool GetCoinMultiplierPower()
    {
        return LoadInternal().coinMultiplier;
    }

    public static void SetDubbleJumpPower(bool status)
    {
         var data = LoadInternal();
        data.isDubbleJump = status;
        SaveInternal( data);
         
    }
      public static void SetShieldPower(bool status)
    {
        var data = LoadInternal();
        data.isShield = status;
         SaveInternal( data);
    }
      public static void SetCoinMultiplierPower(bool status)
    {
        var data = LoadInternal();
        data.coinMultiplier = status;
        SaveInternal( data);
    }

    // --------------------
    // Sound DATA
    // --------------------
   
    public static bool MusicOn
    {
        get => PlayerPrefs.GetInt(GameConstants.MUSIC_KEY, 1) == 1;
        set => PlayerPrefs.SetInt(GameConstants.MUSIC_KEY, value ? 1 : 0);
    }

    public static bool SfxOn
    {
        get => PlayerPrefs.GetInt(GameConstants.SFX_KEY, 1) == 1;
        set => PlayerPrefs.SetInt(GameConstants.SFX_KEY, value ? 1 : 0);
    }

    // --------------------
    // RESET DATA
    // --------------------

    public static void ResetAll()
    {
        PlayerPrefs.DeleteKey(GetKey());
    }
}
