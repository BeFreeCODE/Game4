using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public static Player instance;

	public float rotSpeed;
	public float touchPower;

    private bool waveState = false;

    public GameObject wave;



	void Awake()
	{
		if (instance == null)
			instance = this;
	}
    
	void Start () {
		rotSpeed = 100.0f;
		touchPower = 0.3f;
	}
	
	void Update () {
		RotatePlayer (rotSpeed);

        if(rotSpeed >= 1100 && !waveState)
        {
            MakeWave();
            waveState = true;
        }

        if(waveState)
        {
            rotSpeed -= 200 * Time.deltaTime;
            if(rotSpeed <= 100f)
            {
                waveState = false;
            }
        }
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
    //스피드 다운.
    public void RotateSpeedDown()
    {
        rotSpeed -= 100f;
    }

    //웨이브 생성
    public void MakeWave()
    {
        GameObject newWave = Instantiate(wave);
        newWave.transform.position = Vector3.zero;

    }
}
