using UnityEngine;
using System;
using UnityEngine.UI;
using DG.Tweening;

public class MenuBUD : MonoBehaviour
{
    [SerializeField] Button menuBtn;
    [SerializeField] Button storeBtn;
    [SerializeField] Button settingBtn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        menuBtn.onClick.AddListener(OnMenuBtnClick);
        storeBtn.onClick.AddListener(OnStoreBtnClick);
        settingBtn.onClick.AddListener(OnSettingBtnClick);
    }

    void OnDisable()
    {
        menuBtn.onClick.RemoveListener(OnMenuBtnClick);
        storeBtn.onClick.RemoveListener(OnStoreBtnClick);
        settingBtn.onClick.RemoveListener(OnSettingBtnClick);
    }

    void Start()
    {
        HiglightActiveButton(menuBtn);
        ScreenManager.Instance.Show(ScreenType.Menu);
    }

  

    void OnMenuBtnClick()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.btnClickSound);
        HiglightActiveButton(menuBtn);
        ScreenManager.Instance.Show(ScreenType.Menu);
    }
     void OnStoreBtnClick()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.btnClickSound);
        HiglightActiveButton(storeBtn);
        ScreenManager.Instance.Show(ScreenType.Store);
    }
  
     void OnSettingBtnClick()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.btnClickSound);
        HiglightActiveButton(settingBtn);
        ScreenManager.Instance.Show(ScreenType.Settings);
    }

    public void ShowBUD()
    {
        transform.DOScale(Vector3.one, .5f);
    }
    public void HideBUD()
    {
         transform.DOScale(Vector3.zero, .5f);
    }

    void HiglightActiveButton(Button button)
    {
       
        var mB = menuBtn.GetComponent<RectTransform>();
        mB.sizeDelta = new Vector2(mB.sizeDelta.x, 180);

        var sB = storeBtn.GetComponent<RectTransform>();
        sB.sizeDelta = new Vector2(sB.sizeDelta.x, 180);

        var cB = settingBtn.GetComponent<RectTransform>();
        cB.sizeDelta = new Vector2(cB.sizeDelta.x, 180);

         var thisB = button.GetComponent<RectTransform>();
        thisB.sizeDelta = new Vector2(thisB.sizeDelta.x, 200);
    }
}
