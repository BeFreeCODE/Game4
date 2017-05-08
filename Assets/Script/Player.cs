using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public static Player instance;

	[SerializeField]
	private float rotSpeed;

	public float touchPower;

	void Awake()
	{
		if (instance == null)
			instance = this;
	}

	// Use this for initialization
	void Start () {
		rotSpeed = 100.0f;
		touchPower = 0.3f;
	}
	
	// Update is called once per frame
	void Update () {
		RotatePlayer (rotSpeed);
	}

	//player 회전.
	private void RotatePlayer(float _speed)
	{
		this.transform.Rotate (Vector3.forward * Time.deltaTime * _speed, Space.Self);
	}

	//회전 스피드업.
	public void RotateSpeedUp()
	{
		rotSpeed += 10f;
	}
}
