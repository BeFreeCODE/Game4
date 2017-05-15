using UnityEngine;
using System.Collections;

public class Invert : MonoBehaviour {
    [SerializeField]
    private GameObject sInvert;
    [SerializeField]
    private GameObject pInvert;
    [SerializeField]
    private GameObject boom;

    public bool invertState;
    private float invertTime;

    private void Update()
    {
        if(invertState)
        {
            Time.timeScale = .5f;

            invertTime += Time.deltaTime;

            pInvert.SetActive(true);

            sInvert.SetActive(true);
   
            if(invertTime >= 2f)
            {
                sInvert.SetActive(false);

                boom.GetComponent<TweenScale>().ResetToBeginning();
                boom.GetComponent<TweenScale>().Play();

                boom.SetActive(true);

                invertTime = 0f;

                Time.timeScale = 1f;

                invertState = false;
            }
        }
    }

}
