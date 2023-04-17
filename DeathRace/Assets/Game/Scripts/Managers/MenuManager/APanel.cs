using DG.Tweening;
using UnityEngine;

public abstract class APanel : MonoBehaviour
{ 
    public virtual void Initialize() {

    }
    public virtual void Open() {
        gameObject.SetActive(true);
        GetComponent<CanvasGroup>().DOFade(1, .5f).SetEase(Ease.InCirc);
    }
    public virtual void Close() {
        GetComponent<CanvasGroup>().DOFade(0, .5f).OnComplete(() => CloseNow()).SetEase(Ease.InCirc);
    }
    public void CloseNow() {
        gameObject.SetActive(false);
    }
}
