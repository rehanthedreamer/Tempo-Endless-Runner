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
        public List<string> activePowerUps = new List<string>();
    }

    // --------------------
    // Internal Access
    // --------------------

    private static string GetKey(GameMode mode)
    {
        return GameConstants.SAVE_KEY_PREFIX + mode.ToString();
    }

    private static PlayerSaveData LoadInternal(GameMode mode)
    {
        string key = GetKey(mode);

        if (PlayerPrefs.HasKey(key))
        {
            string json = PlayerPrefs.GetString(key);
            return JsonUtility.FromJson<PlayerSaveData>(json);
        }

        return new PlayerSaveData();
    }

    private static void SaveInternal(GameMode mode, PlayerSaveData data)
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(GetKey(mode), json);
        PlayerPrefs.Save();
    }

    // --------------------
    // PUBLIC Access
    // --------------------

    public static int GetCoins()
    {
        return PlayerPrefs.GetInt(GameConstants.SAVE_TOTAL_COINS);
    }

    public static void AddCoins( int amount)
    {
        PlayerPrefs.SetInt(GameConstants.SAVE_TOTAL_COINS,  GetCoins() + amount);
        PlayerPrefs.Save();
    }

    public static float GetBestDistance(GameMode mode)
    {
        return LoadInternal(mode).bestDistance;
    }

    public static void TrySetBestDistance(GameMode mode, float distance)
    {
        var data = LoadInternal(mode);

        if (distance > data.bestDistance)
        {
            data.bestDistance = distance;
            SaveInternal(mode, data);
        }
    }

    public static List<string> GetActivePowerUps(GameMode mode)
    {
        return LoadInternal(mode).activePowerUps;
    }

    public static void SetActivePowerUps(GameMode mode, List<string> powerUps)
    {
        var data = LoadInternal(mode);
        data.activePowerUps = powerUps;
        SaveInternal(mode, data);
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

    public static void ResetMode(GameMode mode)
    {
        PlayerPrefs.DeleteKey(GetKey(mode));
    }

    public static void ResetAll()
    {
        foreach (GameMode mode in Enum.GetValues(typeof(GameMode)))
        {
            PlayerPrefs.DeleteKey(GetKey(mode));
        }
    }
}
