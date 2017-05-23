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

    public GameObject frag;
    public GameObject invert;

    public Material playerMat;
    public Material trailMat;

    public GameObject effect;

    private Color matColor;

    //색변환
    public void ChangeColor(Color _color, Color _emission)
    {
        playerMat.SetColor("_Color", _color);
        playerMat.SetColor("_EmissionColor", _emission);

        trailMat.SetColor("_TintColor", new Color(_color.r , _color.g , _color.b ));

        matColor = _emission;
    }

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
        if (GameManager.instance.curState != GameState.game)
        {
            playerMat.SetColor("_EmissionColor", matColor);
        }

        RotatePlayer(rotSpeed);

        //make wave
        if (rotSpeed >= 1100 && !waveState)
        {
            MakeWave();
            waveState = true;
        }
        if (waveState)
        {
            rotSpeed -= 500 * Time.deltaTime;
            if (rotSpeed <= 100f)
            {
                rotSpeed = 100f;
                waveState = false;
                upState = false;
            }
        }

        //속도 올려줌.
        if (upState)
        {
            rotSpeed += 2;
            if (rotSpeed >= curSpeed)
            {
                upState = false;
            }
        }
    }

    //깜빡임 이펙트
    private IEnumerator BlinkEffect()
    {
        playerMat.SetColor("_EmissionColor", new Color(0.8f, 0.8f, 0.8f));

        yield return new WaitForSeconds(.1f);

        playerMat.SetColor("_EmissionColor", matColor);
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

        GameObject newEffect = Instantiate(effect);

        StartCoroutine(BlinkEffect());

        newEffect.transform.parent = this.transform;
        newEffect.transform.rotation = this.transform.rotation;
        newEffect.transform.position = this.transform.position + Vector3.forward;
    }

    //스피드 다운.
    public void RotateSpeedDown()
    {
        rotSpeed -= 200f;
        if (rotSpeed <= 0)
        {
            rotSpeed = 1;
        }

        //StartCoroutine(BlinkEffect());
    }

    public void PlayerDead()
    {
        rotSpeed = 0f;
        
        Instantiate(frag);

        this.gameObject.SetActive(false);
    }

    //웨이브 생성
    public void MakeWave()
    {
        invert.GetComponent<Invert>().invertState = true;

        //웨이브 효과로 적 생성속도 느려짐
        EnemyManager.instance.enemyMoveSpeed -= .5f;
    }
}