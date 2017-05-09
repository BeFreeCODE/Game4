using UnityEngine;
using System.Collections;

public enum GameState
{
    main = 0,
    game,
    over
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private GameObject  touchTarget = null;
    public  GameState   curState = GameState.main;
    public  float       gameTime = 0f;
    public int curScore;
    public int topScore;

    [SerializeField]
    private GpgsMng gpgs;


    void Awake()
    {
        if (instance == null)
            instance = this;

        DataManager.Instance.GetData();
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
                    curState = GameState.game;
                }

                break;
            case GameState.game:
                TouchEnemy();
                GameTime();
                CheckSpeed();

                break;
            case GameState.over:
                DataManager.Instance.SetData();
                gpgs.ReportScore(topScore);
                break;
        }
    }

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

            //EnemyScale 줄임.
            if (hit.collider.transform.tag.Equals("Enemy"))
            {
                EnemyManager.instance.AddScalingTarget(touchTarget);
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

        if (EnemyManager.instance.maxScale <= 5f)
            EnemyManager.instance.maxScale += 0.0001f;
    }

    void CheckSpeed()
    {
        //GAME OVER
        if (Player.instance.rotSpeed <= 0f)
        {
            curState = GameState.over;
        }
    }

    public void PlusScore()
    {
        curScore++;

        if (curScore >= topScore)
            topScore = curScore;
    }
}
