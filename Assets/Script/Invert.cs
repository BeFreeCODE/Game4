using UnityEngine;
using System.Collections;

public class Invert : MonoBehaviour {
    [SerializeField]
    private GameObject sInvert;
    [SerializeField]
    private GameObject pInvert;
    [SerializeField]
    private GameObject pInvert2;
    [SerializeField]
    private GameObject pInvert3;

    [SerializeField]
    private GameObject boom;

    public bool invertState;
    private bool pauseState;

    private float invertTime;

    public void PauseGame()
    {
        pauseState = true;
    }

    public void ReplayGame()
    {
        pauseState = false;
    }

    private void Update()
    {
        if (!pauseState)
        {
            if (invertState)
            {
                Time.timeScale = .3f;

                invertTime += Time.deltaTime;

                pInvert.SetActive(true);
                sInvert.SetActive(true);

                if (invertTime >= .15f)
                {
                    pInvert2.SetActive(true);
                }
                if (invertTime >= .3f)
                {
                    pInvert3.SetActive(true);
                }

                if (invertTime >= 0.85f)
                {
                    //BGM 원래속도
                    SoundManager.instance.ModestBGM();
                    SoundManager.instance.PlayEffectSound(5);

                    pInvert.SetActive(false);
                    pInvert2.SetActive(false);
                    pInvert3.SetActive(false);
                    sInvert.SetActive(false);

                    boom.GetComponent<TweenScale>().ResetToBeginning();
                    boom.GetComponent<TweenScale>().Play();

                    boom.SetActive(true);

                    invertTime = 0f;

                    Time.timeScale = 1f;

                    invertState = false;
                }
            }
            else
            {
                pInvert.GetComponent<TweenScale>().ResetToBeginning();
                pInvert.GetComponent<TweenScale>().Play();

                pInvert2.GetComponent<TweenScale>().ResetToBeginning();
                pInvert2.GetComponent<TweenScale>().Play();

                pInvert3.GetComponent<TweenScale>().ResetToBeginning();
                pInvert3.GetComponent<TweenScale>().Play();
            }
        }
    }
}
