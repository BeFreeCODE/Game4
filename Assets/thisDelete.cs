using UnityEngine;
using System.Collections;

public class thisDelete : MonoBehaviour {

    float deleteTime;

	
	// Update is called once per frame
	void Update () {
        deleteTime += Time.deltaTime;
        
        if(deleteTime >= 3f)
        {
            Destroy(this.gameObject);
        }
	}
}
