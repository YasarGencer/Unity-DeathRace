using UnityEngine;

public class StartCutscene : MonoBehaviour {
    [SerializeField] GameObject timeline;
    [SerializeField] Cinemachine.CinemachineVirtualCamera camStart, camMain, canmBetween;

    Transform player;

    private void Start() { 
        MainManager.Instance.Event.onGameStart += OnStart;

        SetReady(); 
        timeline.SetActive(false);
        camMain.gameObject.SetActive(false);
    } 
    public void OnStart() {
        SetReady();
        timeline.SetActive(true);
        camMain.gameObject.SetActive(true);
    }
    void SetReady() {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        camStart.Follow = player;
        camMain.Follow = player;
        canmBetween.Follow = player;

        camStart.LookAt = player;
        camMain.LookAt = player;
        canmBetween.LookAt = player;
    }
}
