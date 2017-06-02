using UnityEngine;
using System.Collections;

public class Logo : MonoBehaviour {
    [SerializeField]
    private float delayTime = 2f;
    private float curTime = 0f;
	

	void Update () {
        curTime += Time.deltaTime;

        if(curTime >= delayTime)
        {
            Application.LoadLevel("Game");
            curTime = 0f;
        }
	}
}
