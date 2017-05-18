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
    private float invertTime;

    private void Update()
    {
        if(invertState)
        {
            Time.timeScale = .3f;

            invertTime += Time.deltaTime;

            pInvert.SetActive(true);
            sInvert.SetActive(true);
    
            if(invertTime >= .15f)
            {
                pInvert2.SetActive(true);
            }
            if(invertTime >= .3f)
            {
                pInvert3.SetActive(true);
            }

            if(invertTime >= 0.85f)
            {
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
