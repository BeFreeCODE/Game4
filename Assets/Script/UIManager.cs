using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {
    public UISprite gage;
    public GameObject mainUI;

    private void Update()
    {
        GameUI();
    }

    private void GameUI()
    {
        switch(GameManager.instance.curState)
        {
            case GameState.main:
                mainUI.SetActive(true);

                break;
            case GameState.game:
                mainUI.SetActive(false);

                gage.fillAmount = (Player.instance.rotSpeed - 100) * 0.001f;
                break;
            case GameState.over:
                break;
        }
    }
}
