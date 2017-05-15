using UnityEngine;
using System.Collections;

public class Fragments : MonoBehaviour {
    public Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb.AddForce(new Vector2(Random.Range(-50f, 50f),
                                Random.Range(20f, 30f)));
        
	}

}
