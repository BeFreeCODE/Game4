using UnityEngine;
using System.Collections;

public enum GameState
{
    main = 0,
    game,
    over,
    store
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public UIManager uiManager;

    private GameObject  touchTarget = null;
    public  GameObject  player;

    public  GameState   curState = GameState.main;

    public  float       gameTime = 0f;

    public int curScore;
    public int topScore;
    public int combo;
    
    [SerializeField]
    private GpgsMng gpgs;

    //ColorSetting
    public int COLORNUM = 0;
    public Material enemyMat;
    public Camera mainCam;

    void Awake()
    {
        if (instance == null)
            instance = this;

        DataManager.Instance.GetData();
    }

    void Start()
    {
        SetColor();

        combo = 0;
    }

    void Update()
    {
        Game();
    }

    void Game()
    {
        switch (curState)
        {
            case GameState.main:
                if(Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    RaycastHit hit;

                    if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
                    {
                        if (hit.collider.name.Equals("col"))
                        {
                            curState = GameState.game;
                        }
                    }
                }
                player.SetActive(true);
                
                break;
            case GameState.game:
                TouchEnemy();
                GameTime();
                CheckSpeed();
                break;
            case GameState.over:
                DataManager.Instance.SetData();
                gpgs.ReportScore(topScore);
                gpgs.ReportProgress(topScore);
                break;
        }
    }

    //적 터치~
    void TouchEnemy()
    {
#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            //클릭 했을때마다 touchPower 받아옴.
            float _touchPower = Player.instance.touchPower;

            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch _touch = Input.GetTouch(i);

                if (_touch.phase.Equals(TouchPhase.Began))
                {
                    Vector2 pos = Camera.main.ScreenToWorldPoint(_touch.position);
                    Ray2D ray = new Ray2D(pos, Vector2.zero);

                    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                    if (hit.transform == null)
                        return;

                    touchTarget = hit.transform.gameObject;

                    //EnemyScale 줄임.
                    if (hit.collider.transform.tag.Equals("Enemy"))
                    {
                        EnemyManager.instance.AddScalingTarget(touchTarget);

                    }
                    else
                    {
                        //combo init
                        combo = 0;
                        Player.instance.RotateSpeedDown();
                        uiManager.PrintMissLabel(pos);

                        uiManager.PrintMissLabel(_touch.position);
                        //camera shake
                        mainCam.GetComponent<CameraShake>().shake = 1;
                    }
                }
            }
        }
#endif
        if (Input.GetMouseButtonDown(0))
        {
            //클릭 했을때마다 touchPower 받아옴.
            float _touchPower = Player.instance.touchPower;

            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Ray2D ray = new Ray2D(pos, Vector2.zero);

            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.transform == null)
                return;

            touchTarget = hit.transform.gameObject;

            Debug.Log("hit : " + touchTarget);

            //EnemyScale 줄임.
            if (hit.collider.transform.tag.Equals("Enemy"))
            {
                EnemyManager.instance.AddScalingTarget(touchTarget); 
            }
            else
            {
                //combo init
                combo = 0;
                Player.instance.RotateSpeedDown();
                uiManager.PrintMissLabel(Input.mousePosition);

                //camera shake
                mainCam.GetComponent<CameraShake>().shake = 1;
            }
        }
    }

    void GameTime()
    {
        //게임시간 흐름(점수)
        gameTime += Time.deltaTime;

        //생성쿨타임을 줄여줌.
        if (EnemyManager.instance.renderTime >= 0.5f)
            EnemyManager.instance.renderTime -= 0.00005f;

        if (EnemyManager.instance.maxScale <= 1.5f)
            EnemyManager.instance.maxScale += 0.0001f;

        if (EnemyManager.instance.bossDelay >= 3f)
            EnemyManager.instance.bossDelay -= 0.0001f;

        EnemyManager.instance.enemyMoveSpeed += 0.0005f;
    }

    void CheckSpeed()
    {
        //GAME OVER
        if (Player.instance.rotSpeed <= 0f)
        {
            curState = GameState.over;
            Player.instance.PlayerDead();
        }
    }

    public void PlusScore()
    {
        curScore++;

        if (curScore >= topScore)
            topScore = curScore;
    }

    void SetColor()
    {
        COLORNUM = PlayerPrefs.GetInt("COLORNUM");

        switch(COLORNUM)
        {
            case 1:
                Player.instance.ChangeColor(new Color(255f/255f, 0f, 0f, 255f/255f), new Color(0.2f, 0f, 0f));
                GameManager.instance.enemyMat.SetColor("_Color", new Color(0f / 255f, 160f / 255f, 255f / 255f, 255f / 255f));
                GameManager.instance.mainCam.backgroundColor = new Color(0f, 0f, 0f, 0f);
                break;
            case 2:
                Player.instance.ChangeColor(new Color(166f/255f, 20f/255f, 47f/255f, 255f/255f), new Color(0.6f, 0.1f, 0.27f));
                GameManager.instance.enemyMat.SetColor("_Color", new Color(217f / 255f, 54f / 255f, 84f / 255f, 255f / 255f));
                GameManager.instance.mainCam.backgroundColor = new Color(217f/255f, 152f/255f, 115f/255f, 0f);
                break;
            case 3:
                Player.instance.ChangeColor(new Color(255f / 255f, 97f / 255f, 56f / 255f, 255f / 255f), new Color(1f, 0.38f, 0.2f));
                GameManager.instance.enemyMat.SetColor("_Color", new Color(0f, 163f / 255f, 136f / 255f, 255f / 255f));
                GameManager.instance.mainCam.backgroundColor = new Color(121f / 255f, 189f / 255f, 143f / 255f, 0f);
                break;
            case 4:
                Player.instance.ChangeColor(new Color(1f, 1f, 1f, 1f), new Color(0.4f, 0.4f, 0.4f));
                GameManager.instance.enemyMat.SetColor("_Color", new Color(255f / 255f, 0f, 0f, 255f));
                GameManager.instance.mainCam.backgroundColor = new Color(242f / 255f, 185f / 255f, 4f / 255f, 0f);
                break;
            case 5:
                Player.instance.ChangeColor(new Color(66f / 255f, 75f / 255f, 84f / 255f, 1f), new Color(0.25f, 0.3f, 0.3f));
                GameManager.instance.enemyMat.SetColor("_Color", new Color(1f, 1f, 1f, 255f));
                GameManager.instance.mainCam.backgroundColor = new Color(14f / 255f, 21f / 255f, 37f / 255f, 0f);
                break;
            case 6:
                Player.instance.ChangeColor(new Color(243f / 255f, 203f / 255f, 73f / 255f, 1f), new Color(0.96f, 0.6f, 0.2f));
                GameManager.instance.enemyMat.SetColor("_Color", new Color(117f / 255f, 66f / 255f, 47f / 255f, 255f / 255f));
                GameManager.instance.mainCam.backgroundColor = new Color(157f / 255f, 17f / 255f, 20f / 255f, 0f);
                break;

        }
    }
}
