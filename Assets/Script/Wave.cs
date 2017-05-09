using UnityEngine;
using System.Collections;

public class Wave : MonoBehaviour {

    public void Delete()
    {
        Destroy(this.transform.gameObject);
    }

}
