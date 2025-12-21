using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : UIScreen
{
    public static Action<float> OnGameOver;
    [SerializeField] Button gotToHomeBtn;
    [SerializeField] Button retryBtn;
    [SerializeField] TMP_Text gameOverText;
    [SerializeField] TMP_Text bestDisText;


    void OnEnable()
    {
        gotToHomeBtn.onClick.AddListener(OnClickGoToHomeBtn);
        retryBtn.onClick.AddListener(OnClickRetryBtn);
        OnGameOver += GameOverDescription;
    }

    void OnDisable()
    {
        gotToHomeBtn.onClick.RemoveListener(OnClickGoToHomeBtn);
        retryBtn.onClick.RemoveListener(OnClickRetryBtn);
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
         if(distance >SaveService.GetBestDistance())
        {
            SaveService.TrySetBestDistance(distance);
            bestDisText.text = "New best distance covered " + Extensions.DistanceFormat(SaveService.GetBestDistance());
        }
        else
            bestDisText.text = "Best distance covered " + Extensions.DistanceFormat(SaveService.GetBestDistance());
        gameOverText.text = "You run " + Extensions.DistanceFormat(distance)+" before hit the barrel!";
        // for game play reset
        gotToHomeBtn.interactable = false;
        retryBtn.interactable = false;
        GameManager.Instance.OnGamePlayRetry();
        StartCoroutine(ActiveBtnAfterAWait());
       }
       IEnumerator ActiveBtnAfterAWait()
    {
        yield return new WaitForSeconds(1.5f);
        gotToHomeBtn.interactable = true;
        retryBtn.interactable = true;
    }

    void OnClickGoToHomeBtn()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.btnClickSound);
        GameManager.Instance.SetState(GameState.inMenu);
    }

    void OnClickRetryBtn()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.btnClickSound);
        GameManager.Instance.SetState(GameState.inGame);
    }
}
