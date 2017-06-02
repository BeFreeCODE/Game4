using UnityEngine;
using System.Collections;

public class Wave : MonoBehaviour {

    public void Delete()
    {
        this.gameObject.SetActive(false);
    }

    public void DestroyThis()
    {
        Destroy(this.gameObject);
    }

    public void PlusGem()
    {
        GameManager.instance.PlusGem();
    }
}
