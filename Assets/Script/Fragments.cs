using UnityEngine;
using System.Collections;

public class Fragments : MonoBehaviour {
    public Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb.AddForce(new Vector3(Random.Range(-70f, 70f),
                                Random.Range(-50f, 50f)));
        
	}

}
