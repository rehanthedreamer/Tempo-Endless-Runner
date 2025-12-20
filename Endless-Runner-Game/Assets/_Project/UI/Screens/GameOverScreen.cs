using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : UIScreen
{
    public static Action<float> OnGameOver;
    [SerializeField] Button gotToHomeBtn;
    [SerializeField] TMP_Text gameOverText;
    [SerializeField] TMP_Text bestDisText;


    void OnEnable()
    {
        gotToHomeBtn.onClick.AddListener(OnClickGoToHomeBtn);
        OnGameOver += GameOverDescription;
    }

    void OnDisable()
    {
        gotToHomeBtn.onClick.RemoveListener(OnClickGoToHomeBtn);
        OnGameOver -= GameOverDescription;
    }
    public override void Show()
    {
        base.Show();
    }

    public override void Hide()
    {
        base.Hide();
    }

   void GameOverDescription(float distance)
    {
         if(distance >SaveService.GetBestDistance(GameManager.Instance.SelectedMode))
        {
            SaveService.TrySetBestDistance(GameManager.Instance.SelectedMode, distance);
            bestDisText.text = "New best distance covered " + Extensions.DistanceFormat(SaveService.GetBestDistance(GameManager.Instance.SelectedMode));
        }
        else
            bestDisText.text = "Best distance covered " + Extensions.DistanceFormat(SaveService.GetBestDistance(GameManager.Instance.SelectedMode));
        gameOverText.text = "You run " + Extensions.DistanceFormat(distance)+" before hit the barrel!";
        GameManager.Instance.OnGamePlayRetry();

       }

    void OnClickGoToHomeBtn()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.btnClickSound);
        // Retry game 
        GameManager.Instance.SetState(GameState.inGame);
    }
}
