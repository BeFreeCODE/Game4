using UnityEngine;
using System.Collections;

public class Gem : MonoBehaviour {
    private void Start()
    {
        this.transform.parent.GetComponent<TweenPosition>().from = this.transform.position;
        this.transform.parent.GetComponent<TweenPosition>().enabled  = false;
        this.transform.parent.GetComponent<TweenScale>().enabled     = false;
    }

    void RotateGem()
    {
        this.transform.Rotate(new Vector3(0,0,90) * Time.deltaTime * -3f
                                           , Space.Self);
    }



    void Update()
    {
        RotateGem();
        
    }
}
