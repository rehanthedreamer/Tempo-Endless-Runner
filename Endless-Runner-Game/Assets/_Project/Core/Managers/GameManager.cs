using System;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static Action<GameModeConfig> OnGameModeChanged;
    public static event Action<GameState> OnGameStateChanged;
    [Header("Game Mode Config")]
    public GameplayData  gameplayData;

    public GameState CurrentState { get; private set; }
    [Header("Game Mode Atributes")]
    [SerializeField] private TMP_Dropdown dropdown;
    public GameMode SelectedMode { get; private set; }
    [SerializeField] Sprite easyModeSprite;
    [SerializeField] Sprite hardModeSprite;
    [SerializeField] UnityEngine.UI.Image modeImage;

     [Header("DistanceTracker")]
     public DistanceTracker distanceTracker;

    void Start() 
    {
        SetState(GameState.inMenu);
        SetupDropdown();
        dropdown.onValueChanged.AddListener(OnModeSelected);
        //set default mode
         OnModeSelected(0);
    }

    void GameModeSprite()
    {
       modeImage.sprite = SelectedMode == GameMode.Easy 
        ? easyModeSprite 
        : hardModeSprite;
    }

    public void SetState(GameState newState)
    {
        if (CurrentState == newState)
            return;

        CurrentState = newState;
        OnGameStateChanged?.Invoke(newState);
    }

    private void SetupDropdown()
    {
        dropdown.ClearOptions();
        dropdown.AddOptions(new System.Collections.Generic.List<string>
        {
            "Easy",
            "Hard"
        });

        dropdown.value = 0;
        SelectedMode = GameMode.Easy;
        GameModeSprite();
    }

    private void OnModeSelected(int index)
    {
        SelectedMode = (GameMode)index;
        Debug.Log("Selected Mode: " + SelectedMode);
        GameModeSprite();
        OnGameModeChanged?.Invoke(gameplayData.GetGameModeData(SelectedMode));
    }

    private void OnDestroy()
    {
        dropdown.onValueChanged.RemoveListener(OnModeSelected);
    }

    public void OnGamePlayRetry()
    {
        distanceTracker.gameObject.transform.position =GameConstants.PLAYERPOS;
        distanceTracker.ResetDistance();
        distanceTracker.transform.GetComponent<PlayerCollision>().ResetPlayer();
        PlatformManager.Instance.initialPlatform[0].transform.position = GameConstants.P1_Pos;
        PlatformManager.Instance.initialPlatform[1].transform.position =  GameConstants.P2_Pos;
        PlatformManager.Instance.initialPlatform[2].transform.position =  GameConstants.P3_Pos;
        PlatformManager.Instance.initialPlatform[0].OnSpawned();
        PlatformManager.Instance.initialPlatform[1].OnSpawned();
        PlatformManager.Instance.initialPlatform[2].OnSpawned();
        PlatformManager.Instance.ReturnAllSpawnedToPool();
        ObstacleSpawner.Instance.ReturnAllSpawnedToPool();
        CoinSpawner.Instance.ReturnAllSpawnedToPool();
        SoundManager.Instance.StopSFX();
        DifficultyManager.Instance.ResetDifficulty();
    }
}
