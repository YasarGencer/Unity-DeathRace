using DG.Tweening;
using TMPro; 
using UnityEngine;
using UnityEngine.EventSystems; 

public class GamePanel : APanel, IPointerDownHandler, IDragHandler, IPointerUpHandler {
    [SerializeField] TextMeshProUGUI _countdonwText;     
    
    public override void Initialize() {
        base.Initialize();
        _countdonwText.gameObject.SetActive(false);
    }
    public override void Open() {
        base.Open();
        StartCountDown();
    }
    public override void Close() {
        base.Close();
    }
    #region gameplay
    Vector2 _firstTouch;
    PlayerController player = null;
    public void OnPointerDown(PointerEventData eventData) {
        _firstTouch = eventData.position;

        if (player != null)
            return;
        player = GameObject.FindObjectOfType<PlayerController>();
    }
    public void OnDrag(PointerEventData eventData) {
        if (player == null)
            return;

        if (eventData.position.x > _firstTouch.x)
            player.Swipe.SwipeInfo(1);
        else if (eventData.position.x < _firstTouch.x)
            player.Swipe.SwipeInfo(-1);
        else
            player.Swipe.SwipeInfo(0);
    }

    public void OnPointerUp(PointerEventData eventData) {
        if (player == null)
            return;
        player.Swipe.SwipeInfo(0);
    }
    #endregion
    public void StartCountDown() {
        _countdonwText.gameObject.SetActive(true);
        _countdonwText.transform.DOScale(new Vector3(0, 0, 1), 0);

        _countdonwText.SetText("3");

        Sequence sequence = DOTween.Sequence();

        sequence.Append(_countdonwText.transform.DOScale(new Vector3(1, 1, 1), .35f));
        sequence.AppendInterval(.25f);
        sequence.Append(_countdonwText.transform.DOScale(new Vector3(0, 0, 1), .25f))
            .AppendCallback(() => _countdonwText.SetText("2")); 

        sequence.Append(_countdonwText.transform.DOScale(new Vector3(1, 1, 1), .35f));
        sequence.AppendInterval(.25f);
        sequence.Append(_countdonwText.transform.DOScale(new Vector3(0, 0, 1), .25f))
            .AppendCallback(() => _countdonwText.SetText("1"));

        sequence.Append(_countdonwText.transform.DOScale(new Vector3(1, 1, 1), .35f));
        sequence.AppendInterval(.25f);
        sequence.Append(_countdonwText.transform.DOScale(new Vector3(0, 0, 1), .25f))
            .AppendCallback(() => _countdonwText.SetText("GO"));

        sequence.Append(_countdonwText.transform.DOScale(new Vector3(1, 1, 1), .35f));
        sequence.AppendInterval(.25f);
        sequence.Append(_countdonwText.transform.DOScale(new Vector3(0, 0, 1), .25f));

        sequence.Play().OnComplete(()=> _countdonwText.gameObject.SetActive(false));
    }
}
