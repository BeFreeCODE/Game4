using UnityEngine;
using System.Collections;

public class Wave : MonoBehaviour {
    [SerializeField]
    private float waveSpeed = 5f;

    private void Update()
    {
        transform.localScale = Vector3.Slerp(transform.localScale, new Vector3(15, 15, 1), waveSpeed * Time.deltaTime);

        if(this.transform.localScale.x >= 10f)
        {
            Destroy(this.transform.gameObject);
            
        }
    }

}
