using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {
    public UISprite gage;
    public UILabel score;
    public UILabel score2;

    public UILabel topScore;
    public UILabel topScore2;

    public GameObject mainUI;
    public GameObject gameUI;
    public GameObject overUI;

    bool pauseState = false;

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
                gameUI.SetActive(false);
                overUI.SetActive(false);

                topScore.text = GameManager.instance.topScore.ToString();
                break;
            case GameState.game:
                mainUI.SetActive(false);
                gameUI.SetActive(true);

                gage.fillAmount = (Player.instance.rotSpeed - 100) * 0.001f;
                score.text = GameManager.instance.curScore.ToString();
                break;
            case GameState.over:
                gameUI.SetActive(false);
                overUI.SetActive(true);

                score2.text = GameManager.instance.curScore.ToString();
                topScore2.text = GameManager.instance.topScore.ToString();
                break;
        }
    }

    public void Pause()
    {
        if (!pauseState)
        {
            Time.timeScale = 0f;
            pauseState = true;
        }
        else
        {
            Time.timeScale = 1f;
            pauseState = false;
        }
    }

    public void Home()
    {
        //enemy Init
        EnemyManager.instance.InitEnemys();

        //player Init
        Player.instance.rotSpeed = 100f;

        //data init;
        GameManager.instance.curScore = 0;
        GameManager.instance.curState = GameState.main;
    }
}
