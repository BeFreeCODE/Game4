using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public Camera uiCam;

    public UISprite gage;
    public UILabel score;
    public UILabel score2;

    public UILabel topScore;
    public UILabel topScore2;
    public UILabel topScore3;
    public UILabel title;
    public UILabel title2;
    public UILabel title3;
    public UILabel gem;

    public GameObject rankButton;
    public GameObject achiveButton;
    public GameObject homeButton;
    public GameObject homeButton2;

    public GameObject pauseButton;
    public GameObject storeButton;
    public GameObject giftButton;
    public GameObject facebookButton;

    public GameObject muteButton;
    public GameObject soundButton;

    public GameObject mainUI;
    public GameObject gameUI;
    public GameObject overUI;
    public GameObject storeUI;

    bool pauseState = false;

    public GameObject missLabel;
    public GameObject comboLabel;
    public GameObject pausePop;

    private void Update()
    {
        GameUI();
    }

    private void GameUI()
    {
        switch (GameManager.instance.curState)
        {
            case GameState.main:
                mainUI.SetActive(true);
                gameUI.SetActive(false);
                overUI.SetActive(false);
                storeUI.SetActive(false);

                topScore.text = GameManager.instance.topScore.ToString();

                TweenInit();

                title3.GetComponent<TweenPosition>().ResetToBeginning();
                title3.GetComponent<TweenPosition>().Play();
                homeButton2.GetComponent<TweenPosition>().ResetToBeginning();
                homeButton2.GetComponent<TweenPosition>().Play();
                giftButton.GetComponent<TweenScale>().ResetToBeginning();
                giftButton.GetComponent<TweenScale>().Play();
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
        gem.text = GameManager.instance.gem.ToString();
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

        facebookButton.GetComponent<TweenPosition>().ResetToBeginning();
        facebookButton.GetComponent<TweenPosition>().Play();

        topScore3.GetComponent<TweenPosition>().ResetToBeginning();
        topScore3.GetComponent<TweenPosition>().Play();
    }

    public void Pause()
    {
        if (!pauseState)
        {
            pauseButton.SetActive(false);
            pausePop.SetActive(true);
            Time.timeScale = 0f;
            pauseState = true;
        }
    }

    public void RePlay()
    {
        pauseButton.SetActive(true);
        pausePop.SetActive(false);
        Time.timeScale = 1f;
        pauseState = false;

    }

    public void MuteButton()
    {
        soundButton.SetActive(true);
        muteButton.SetActive(false);
    }

    public void SoundButton()
    {
        soundButton.SetActive(false);
        muteButton.SetActive(true);
    }

    public void Home()
    {
        //enemy Init
        EnemyManager.instance.InitEnemys();

        //player Init
        Player.instance.rotSpeed = 100f;

        //data init;
        GameManager.instance.curScore = 0;
        GameManager.instance.combo = 0;
        GameManager.instance.curState = GameState.main;
    }

    public void Store()
    {
        GameManager.instance.curState = GameState.store;
    }

    public void Facebook()
    {
        Application.OpenURL("https://www.facebook.com/60Celsius/?fref=ts");
    }

    public void Share()
    {
        Share share = new Share();

        share.shareText("r o t t a n g l e\n", "Can U Do This??\n" + GameManager.instance.topScore + "\nURL");
    }

    public void PrintMissLabel(Vector3 _pos)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(_pos);

        GameObject newlabel = Instantiate(missLabel);
        newlabel.transform.parent = this.transform;
        newlabel.transform.localScale = new Vector3(1, 1, 1);
        newlabel.transform.position = uiCam.ScreenToWorldPoint(_pos);


        newlabel.GetComponent<TweenPosition>().from = newlabel.transform.localPosition;
        newlabel.GetComponent<TweenPosition>().to = newlabel.transform.localPosition + Vector3.up * 600;
    }

    public void PrintComboLabel(int _num)
    {
        GameObject newlabel = Instantiate(comboLabel);
        newlabel.transform.parent = this.transform;

        newlabel.transform.localScale = new Vector3(1, 1, 1);
        if (_num % 50 == 0)
        {
            newlabel.transform.localScale = new Vector3(2, 2, 2);
        }
        newlabel.transform.position = Vector3.zero;

        newlabel.GetComponent<UILabel>().text = "x" + _num;

        newlabel.GetComponent<TweenPosition>().from = newlabel.transform.localPosition;
        newlabel.GetComponent<TweenPosition>().to = newlabel.transform.localPosition + Vector3.up * 600;
    }
}
