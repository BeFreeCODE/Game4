using UnityEngine;
using System.Collections;

public class GridSelect : MonoBehaviour
{
    public int[] BuyState = new int[6];
    public GameObject Lock;
    public int buyNum = 0;


    private void Awake()
    {
        for (int i = 0; i < BuyState.Length; i++)
        {
            BuyState[i] = 0;
        }
        BuyState[0] = 1;
    }

    private void Start()
    {
        GetBuyData();
    }

    private void SetBuyData()
    {
        for (int i = 0; i < BuyState.Length; i++)
        {
            PlayerPrefs.SetInt("BUYSTATE" + i.ToString(), BuyState[i]);
            DataManager.Instance.SetData();
        }
    }

    private void GetBuyData()
    {
        for (int i = 0; i < BuyState.Length; i++)
        {
            if (i == 0)
            {
                BuyState[i] = 1;
            }
            else
            {
                BuyState[i] = PlayerPrefs.GetInt("BUYSTATE" + i.ToString());
            }
        }
    }

    //스킨 구입
    public void BuySkins()
    {
        if (GameManager.instance.gem >= 100)
        {
            SoundManager.instance.PlayEffectSound(7);

            GameManager.instance.gem -= 100;
            BuyState[buyNum] = 1;

            Lock.SetActive(false);

            //구입후 데이터 저장
            SetBuyData();
        }
        else
        {
            Debug.Log("More Gem!");
        }
    }

    //홈버튼으로 메인상태로 나갈때 색 저장.
    public void HomeCheck()
    {
        if(BuyState[buyNum] == 0)
        {
            Player.instance.ChangeColor(new Color(255f / 255f, 0f, 0f, 255f / 255f), new Color(0.2f, 0f, 0f));
            GameManager.instance.enemyMat.SetColor("_Color", new Color(0f / 255f, 160f / 255f, 255f / 255f, 255f / 255f));
            GameManager.instance.enemyBossMat.SetColor("_Color", new Color(0f / 255f, 160f * 0.3f / 255f, 255f / 255f, 255f / 255f));
            GameManager.instance.mainCam.backgroundColor = new Color(0f, 0f, 0f, 0f);

            GameManager.instance.COLORNUM = 1;

            PlayerPrefs.SetInt("COLORNUM", 1);
        }
        else
        {
            PlayerPrefs.SetInt("COLORNUM", buyNum + 1);
        }
    }

    //중앙 박스 충돌로 색변경.
    private void OnTriggerEnter(Collider col)
    {
        SoundManager.instance.PlayEffectSound(6);

        switch (col.transform.name)
        {
            case "1":
                buyNum = 0;
                GameManager.instance.COLORNUM = 1;
                if (BuyState[0] == 1)
                {
                    Lock.SetActive(false);
                }
                else
                {
                    Lock.SetActive(true);
                }

                Player.instance.ChangeColor(new Color(255f / 255f, 0f, 0f, 255f / 255f), new Color(0.2f, 0f, 0f));
                GameManager.instance.enemyMat.SetColor("_Color", new Color(0f / 255f, 160f / 255f, 255f / 255f, 255f / 255f));
                GameManager.instance.enemyBossMat.SetColor("_Color", new Color(0f / 255f, 160f * 0.3f / 255f, 255f / 255f, 255f / 255f));
                GameManager.instance.mainCam.backgroundColor = new Color(0f, 0f, 0f, 0f);
                break;
            case "2":
                buyNum = 1;
                GameManager.instance.COLORNUM = 2;
                if (BuyState[1] == 1)
                {
                    Lock.SetActive(false);
                }
                else
                {
                    Lock.SetActive(true);
                }
                Player.instance.ChangeColor(new Color(166f / 255f, 20f / 255f, 47f / 255f, 255f / 255f), new Color(0.6f, 0.1f, 0.27f));
                GameManager.instance.enemyMat.SetColor("_Color", new Color(217f / 255f, 54f / 255f, 84f / 255f, 255f / 255f));
                GameManager.instance.enemyBossMat.SetColor("_Color", new Color(217f / 255f, 54f * 0.3f / 255f, 255f / 255f, 255f / 255f));
                GameManager.instance.mainCam.backgroundColor = new Color(217f / 255f, 152f / 255f, 115f / 255f, 0f);
                break;
            case "3":
                buyNum = 2;
                GameManager.instance.COLORNUM = 3;
                if (BuyState[2] == 1)
                {
                    Lock.SetActive(false);
                }
                else
                {
                    Lock.SetActive(true);
                }
                Player.instance.ChangeColor(new Color(255f / 255f, 97f / 255f, 56f / 255f, 255f / 255f), new Color(1f, 0.38f, 0.2f));
                GameManager.instance.enemyMat.SetColor("_Color", new Color(0f, 163f / 255f, 136f / 255f, 255f / 255f));
                GameManager.instance.enemyBossMat.SetColor("_Color", new Color(0f, 163f * 0.3f / 136, 255f / 255f, 255f / 255f));
                GameManager.instance.mainCam.backgroundColor = new Color(121f / 255f, 189f / 255f, 143f / 255f, 0f);
                break;
            case "4":
                buyNum = 3;
                GameManager.instance.COLORNUM = 4;
                if (BuyState[3] == 1)
                {
                    Lock.SetActive(false);
                }
                else
                {
                    Lock.SetActive(true);
                }
                Player.instance.ChangeColor(new Color(1f, 1f, 1f, 1f), new Color(0.4f, 0.4f, 0.4f));
                GameManager.instance.enemyMat.SetColor("_Color", new Color(255f / 255f, 0f, 0f, 1f));
                GameManager.instance.enemyBossMat.SetColor("_Color", new Color(1f, 1f, 1f, 255f));
                GameManager.instance.mainCam.backgroundColor = new Color(0 / 255f, 80f / 255f, 170f / 255f, 0f);
                break;
            case "5":
                buyNum = 4;
                GameManager.instance.COLORNUM = 5;
                if (BuyState[4] == 1)
                {
                    Lock.SetActive(false);
                }
                else
                {
                    Lock.SetActive(true);
                }
                Player.instance.ChangeColor(new Color(66f / 255f, 75f / 255f, 84f / 255f, 1f), new Color(0.25f, 0.3f, 0.3f));
                GameManager.instance.enemyMat.SetColor("_Color", new Color(1f, 1f, 1f, 1f));
                GameManager.instance.enemyBossMat.SetColor("_Color", new Color(1f, 0.3f, 1f, 255f));
                GameManager.instance.mainCam.backgroundColor = new Color(14f / 255f, 21f / 255f, 37f / 255f, 0f);
                break;
            case "6":
                buyNum = 5;
                GameManager.instance.COLORNUM = 6;
                if (BuyState[5] == 1)
                {
                    Lock.SetActive(false);
                }
                else
                {
                    Lock.SetActive(true);
                }
                Player.instance.ChangeColor(new Color(243f / 255f, 203f / 255f, 73f / 255f, 1f), new Color(0.96f, 0.6f, 0.2f));
                GameManager.instance.enemyMat.SetColor("_Color", new Color(117f / 255f, 66f / 255f, 47f / 255f, 255f / 255f));
                GameManager.instance.enemyBossMat.SetColor("_Color", new Color(117f / 255f, 66f * .3f / 255f, 47f / 255f, 255f / 255f));
                GameManager.instance.mainCam.backgroundColor = new Color(157f / 255f, 17f / 255f, 20f / 255f, 0f);
                break;
        }
    }
}
