using UnityEngine;
using System.Collections;

public class Present : MonoBehaviour
{
    public GameObject giftButton;
    public BoxCollider giftBox;

    System.DateTime pStartTime;
    System.DateTime pCurTime;
    System.TimeSpan timeCal;

    int timeCalMinute;
    int timeCalSecond;

    public int pState = 0;
    private int limitGem;
    private bool limitState;

    public UILabel minuteCal;
    public GameObject giftGem;

    private void Start()
    {
        pState = PlayerPrefs.GetInt("PSTATE");

        if (pState == 1)
        {
            //저장했던 시작 시간을 불러옴.
            pStartTime = new System.DateTime(PlayerPrefs.GetInt("PYEAR"),
                                           PlayerPrefs.GetInt("PMONTH"),
                                           PlayerPrefs.GetInt("PDAY"),
                                           PlayerPrefs.GetInt("PHOUR"),
                                           PlayerPrefs.GetInt("PMINUTE"),
                                           PlayerPrefs.GetInt("PSECOND"));

            Debug.Log("STARTTIME : " + pStartTime);
        }
    }

    private void Update()
    {
        if (GameManager.instance.curState == GameState.over)
        {
            if (pState == 1)
            {
                giftButton.SetActive(false);
                giftBox.enabled = false;

                //시간비교
                CompareTime();
            }
            else
            {
                giftButton.SetActive(true);
                giftBox.enabled = true;
                minuteCal.text = "";
            }
        }

        if (limitState)
        {
            if (limitGem > GameManager.instance.gem)
            {
                StartCoroutine(PlusGem());
            }
            if(limitGem == GameManager.instance.gem)
            {
                limitState = false;
            }
        }
    }

    //시간비교.
    private void CompareTime()
    {
        pCurTime = System.DateTime.Now;

        timeCal = (pCurTime - pStartTime);

        //분 비교.
        timeCalMinute = timeCal.Minutes;
        //초 비교.
        timeCalSecond = timeCal.Seconds;


        //시간출력
        minuteCal.text = (59 - timeCalMinute).ToString() 
                            + " : "
                            + (59 - timeCalSecond).ToString();

        //시간차가 0보다 작을때(GIFT활성화)
        if((60-timeCalMinute) <= 0 || timeCal.Hours >= 1 || timeCal.Days >= 1)
        {
            pState = 0;
            PlayerPrefs.SetInt("PSTATE", pState);
        }
    }

    //시작시간 저장.
    public void SetStartTime()
    {
        SoundManager.instance.PlayEffectSound(1);

        giftButton.SetActive(false);
        giftBox.enabled = false;

        pStartTime = System.DateTime.Now;

        //상자를 연 시간 저장.
        PlayerPrefs.SetInt("PYEAR", pStartTime.Year);
        PlayerPrefs.SetInt("PMONTH", pStartTime.Month);
        PlayerPrefs.SetInt("PDAY", pStartTime.Day);
        PlayerPrefs.SetInt("PHOUR", pStartTime.Hour);
        PlayerPrefs.SetInt("PMINUTE", pStartTime.Minute);
        PlayerPrefs.SetInt("PSECOND", pStartTime.Second);

        limitGem = GameManager.instance.gem + 30;
        limitState = true;

        //gem효과 생성
        Instantiate(giftGem);

        pState = 1;
        PlayerPrefs.SetInt("PSTATE", pState);
    }

    private IEnumerator PlusGem()
    {
        GameManager.instance.PlusGem();

        yield return new WaitForSeconds(.1f);
    }
}
