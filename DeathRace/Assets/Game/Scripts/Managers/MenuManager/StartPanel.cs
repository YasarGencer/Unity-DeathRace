using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartPanel : APanel, IPointerDownHandler
{
    public override void Initialize() {
        base.Initialize();
    }
    public override void Open() {
        base.Open();
    }
    public override void Close() {
        base.Close();
    }

    public void OnPointerDown(PointerEventData eventData) { 
        Close();
        MainManager.Instance.Game.StartGame();
        MainManager.Instance.Menu.GamePanel.Open();
    }
}
