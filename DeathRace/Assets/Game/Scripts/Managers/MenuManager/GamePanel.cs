using UnityEngine;
using UnityEngine.EventSystems; 

public class GamePanel : APanel, IPointerDownHandler, IDragHandler, IPointerUpHandler{ 
    public override void Initialize() {
        base.Initialize();
    }
    public override void Open() {
        base.Open(); 
    }
    public override void Close() {
        base.Close();
    }


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
}
