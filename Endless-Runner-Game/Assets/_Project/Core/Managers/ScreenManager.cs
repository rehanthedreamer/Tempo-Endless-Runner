using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using DG.Tweening;

public class ScreenManager : Singleton<ScreenManager>
{
    [SerializeField] private List<UIScreen> screens;
    private Dictionary<ScreenType, UIScreen> _screenMap;
    private UIScreen _currentScreen;
    [Header("Player Animation")]
    [SerializeField] MenuBUD menuBUD;
    [SerializeField] GameObject gamePlayHUD;

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += HandleGameState;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= HandleGameState;
    }

    private void Awake()
    {
        _screenMap = new Dictionary<ScreenType, UIScreen>();

        foreach (var screen in screens)
        {
            _screenMap[screen.ScreenType] = screen;
            screen.Hide();
        }
        HandleGameState(GameState.inMenu);
    }

/// <summary>
/// show screen function
/// </summary>
/// <param name="type"></param>
    public void Show(ScreenType type)
    {
        if (_currentScreen != null)
            _currentScreen.Hide();

        if (_screenMap.TryGetValue(type, out UIScreen screen))
        {
            _currentScreen = screen;
            _currentScreen.Show();
        }
    }
/// <summary>
/// hide _currentScreen
/// </summary>
    public void CloseCurrent()
    {
        if (_currentScreen == null)
            return;

        _currentScreen.Hide();
        _currentScreen = null;
    }

    /// <summary>
    /// Handle screen as per game state 
    /// </summary>
    /// <param name="state"></param>
    private void HandleGameState(GameState state)
    {
        switch (state)
        {
            case GameState.inMenu:
                // Show menu UI
                Show(ScreenType.Menu);
                menuBUD.ShowBUD();
                gamePlayHUD.GetComponent<RectTransform>().DOScale(Vector3.zero, .0f);
                break;

            case GameState.inGame:
                // Start gameplay
                CloseCurrent();
                menuBUD.HideBUD();
                gamePlayHUD.GetComponent<RectTransform>().DOScale(Vector3.one, .0f);
                SoundManager.Instance.PlayBGMusic(SoundManager.Instance.bgMusic);
                break;

            case GameState.GameOver:
                // Show game over screen
                Show(ScreenType.GameOver);
                GameOverScreen.OnGameOver?.Invoke(GameManager.Instance.distanceTracker.Distance);
                gamePlayHUD.GetComponent<RectTransform>().DOScale(Vector3.zero, 0f);
                SoundManager.Instance.StopBGMusic();
                break;
        }
    }
}
