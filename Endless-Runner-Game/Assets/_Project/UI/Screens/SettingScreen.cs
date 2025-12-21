using UnityEngine;
using UnityEngine.UI;

public class SettingScreen : UIScreen
{
    [SerializeField] Button resetDataBtn;
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle sfxToggle;

    void OnEnable()
    {
        resetDataBtn.onClick.AddListener(OnResetData);
        musicToggle.onValueChanged.AddListener(OnMusicToggle);
        sfxToggle.onValueChanged.AddListener(OnSfxToggle);
    }

    void OnDisable()
    {
        resetDataBtn.onClick.RemoveListener(OnResetData);
        musicToggle.onValueChanged.RemoveListener(OnMusicToggle);
        sfxToggle.onValueChanged.RemoveListener(OnSfxToggle);
    }

    private void Start()
    {
        musicToggle.isOn = SaveService.MusicOn;
        sfxToggle.isOn = SaveService.SfxOn;
        
    }

    public void OnMusicToggle(bool isOn)
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.btnClickSound);
        SaveService.MusicOn = isOn;
        SoundManager.Instance.ApplySettings();
    }

    public void OnSfxToggle(bool isOn)
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.btnClickSound);
        SaveService.SfxOn = isOn;
        SoundManager.Instance.ApplySettings();
    }

    public void OnResetData()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.btnClickSound);
        SaveService.ResetAll();
        SoundManager.Instance.ApplySettings();
        CoinHUD.OnCoinUICoinUpdate?.Invoke(0);

    }
}
