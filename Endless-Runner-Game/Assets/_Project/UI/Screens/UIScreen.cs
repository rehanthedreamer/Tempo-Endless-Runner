using DG.Tweening;
using UnityEngine;

public abstract class UIScreen : MonoBehaviour
{
    public ScreenType ScreenType;

    public virtual void Show()
    {
        // gameObject.SetActive(true);
        gameObject.transform.DOScale(Vector3.one, .5f);
    }

    public virtual void Hide()
    {
        // gameObject.SetActive(false);
        gameObject.transform.DOScale(Vector3.zero, .5f);
    }
}
