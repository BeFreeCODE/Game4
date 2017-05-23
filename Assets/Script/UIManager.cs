using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {
    public Camera uiCam;

    public UISprite gage;
    public UILabel score;
    public UILabel score2;

    public UILabel topScore;
    public UILabel topScore2;
    public UILabel topScore3;
    public UILabel title;
    public UILabel title2;

    public GameObject rankButton;
    public GameObject achiveButton;
    public GameObject homeButton;
    public GameObject pauseButton;
    public GameObject storeButton;

    public GameObject mainUI;
    public GameObject gameUI;
    public GameObject overUI;
    public GameObject storeUI;

    bool pauseState = false;
    
    public GameObject missLabel;
    public GameObject comboLabel;

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
                storeUI.SetActive(false);

                topScore.text = GameManager.instance.topScore.ToString();

                TweenInit();
                break;
            case GameState.game:
                mainUI.SetActive(false);
                gameUI.SetActive(true);

                gage.fillAmount = (Player.instance.rotSpeed - 100) * 0.001f;
                score.text = GameManager.instance.curScore.ToString();
                topScore3.text = GameManager.instance.topScore.ToString();
                break;
            case GameState.over:
                gameUI.SetActive(false);
                overUI.SetActive(true);
                
                gage.fillAmount = (Player.instance.rotSpeed - 100) * 0.001f;
                score2.text = GameManager.instance.curScore.ToString();
                topScore2.text = GameManager.instance.topScore.ToString();

                //메인 UI Tween 초기화
                title2.GetComponent<TweenScale>().ResetToBeginning();
                title2.GetComponent<TweenScale>().Play();

                storeButton.GetComponent<TweenAlpha>().ResetToBeginning();
                storeButton.GetComponent<TweenAlpha>().Play();
                break;
            case GameState.store:
                storeUI.SetActive(true);
                mainUI.SetActive(false);
                gameUI.SetActive(false);
                overUI.SetActive(false);

                //메인 UI Tween 초기화
                title2.GetComponent<TweenScale>().ResetToBeginning();
                title2.GetComponent<TweenScale>().Play();

                storeButton.GetComponent<TweenAlpha>().ResetToBeginning();
                storeButton.GetComponent<TweenAlpha>().Play();
                break;
        }
    }

    private void TweenInit()
    {
        title.GetComponent<TweenPosition>().ResetToBeginning();
        title.GetComponent<TweenPosition>().Play();

        pauseButton.GetComponent<TweenPosition>().ResetToBeginning();
        pauseButton.GetComponent<TweenPosition>().Play();

        rankButton.GetComponent<TweenPosition>().ResetToBeginning();
        rankButton.GetComponent<TweenPosition>().Play();

        homeButton.GetComponent<TweenPosition>().ResetToBeginning();
        homeButton.GetComponent<TweenPosition>().Play();

        achiveButton.GetComponent<TweenPosition>().ResetToBeginning();
        achiveButton.GetComponent<TweenPosition>().Play();

        topScore3.GetComponent<TweenPosition>().ResetToBeginning();
        topScore3.GetComponent<TweenPosition>().Play();
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

    public void Store()
    {
        GameManager.instance.curState = GameState.store;
    }

    public void PrintMissLabel(Vector3 _pos)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(_pos);

        GameObject newlabel = Instantiate(missLabel);
        newlabel.transform.parent = this.transform;
        newlabel.transform.localScale = new Vector3(1, 1, 1);
        newlabel.transform.position = uiCam.ScreenToWorldPoint( _pos);


        newlabel.GetComponent<TweenPosition>().from = newlabel.transform.localPosition;
        newlabel.GetComponent<TweenPosition>().to = newlabel.transform.localPosition + Vector3.up * 600;
    }

    public void PrintComboLabel(int _num)
    {
        GameObject newlabel = Instantiate(comboLabel);
        newlabel.transform.parent = this.transform;
        newlabel.transform.localScale = new Vector3(1, 1, 1);
        newlabel.transform.position = Vector3.zero;

        newlabel.GetComponent<UILabel>().text = "x" + _num;

        newlabel.GetComponent<TweenPosition>().from = newlabel.transform.localPosition;
        newlabel.GetComponent<TweenPosition>().to = newlabel.transform.localPosition + Vector3.up * 600;
    }
}
