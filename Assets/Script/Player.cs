using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public static Player instance;

    public float rotSpeed;
    public float touchPower;
    private float curSpeed;

    private bool waveState = false;
    public bool upState = false;

    public GameObject wave;
    public GameObject frag;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        rotSpeed = 100f;
        touchPower = 1f;
    }

    void Update()
    {
        RotatePlayer(rotSpeed);

        //make wave
        if (rotSpeed >= 1100 && !waveState)
        {
            MakeWave();
            waveState = true;
        }
        if (waveState)
        {
            rotSpeed -= 200 * Time.deltaTime;
            if (rotSpeed <= 100f)
            {
                rotSpeed = 100f;
                waveState = false;
            }
        }

        if (upState)
        {
            rotSpeed += 2;
            if (rotSpeed >= curSpeed)
            {
                upState = false;
            }
        }
    }

    //player 회전.
    private void RotatePlayer(float _speed)
    {
        this.transform.Rotate(Vector3.forward * Time.deltaTime * _speed, Space.Self);
    }

    //회전 스피드업.
    public void RotateSpeedUp()
    {
        curSpeed = rotSpeed + 20;
        upState = true;
    }

    //스피드 다운.
    public void RotateSpeedDown()
    {
        rotSpeed -= 100f;
        if (rotSpeed <= 0)
        {
            rotSpeed = 0;
        }
    }

    public void PlayerDead()
    {
        Instantiate(frag);

        this.gameObject.SetActive(false);
    }

    //웨이브 생성
    public void MakeWave()
    {
        GameObject newWave = Instantiate(wave);
        newWave.transform.position = Vector3.zero;

    }
}