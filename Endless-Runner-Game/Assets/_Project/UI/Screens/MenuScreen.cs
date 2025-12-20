using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : UIScreen
{
    [SerializeField] Button playBtn;

    void OnEnable()
    {
        playBtn.onClick.AddListener(OnClickPlayBtn);
    }

    void OnDisable()
    {
         playBtn.onClick.RemoveListener(OnClickPlayBtn);
    }
    public override void Show()
    {
        base.Show();
    }

    public override void Hide()
    {
        base.Hide();
    }

    void OnClickPlayBtn()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.btnClickSound);
        GameManager.Instance.SetState(GameState.inGame);
    }
}
