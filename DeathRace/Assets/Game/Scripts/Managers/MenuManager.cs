using UniRx.Triggers;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] APanel _startPanel, _gamePanel;
    public APanel StartPanel { get { return _startPanel; } }
    public APanel GamePanel { get { return _gamePanel; } }
    public void Initialize() { 

        _startPanel.Initialize();
        _gamePanel.Initialize();

        _startPanel.Open();
        _gamePanel.CloseNow();
    } 
}
