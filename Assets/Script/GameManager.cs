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

	private GameObject touchTarget = null;
	public GameState curState = GameState.main;
	public float gameTime = 0f;

	void Awake()
	{
		if (instance == null)
			instance = this;
	}

	void Update()
	{
		Game ();
	}

	void Game()
	{
		switch (curState) {
		case GameState.main:
			break;
		case GameState.game:		
			TouchEnemy ();
			GameTime ();
			break;
		case GameState.over:
			break;
		}
	}

	void TouchEnemy()
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			//클릭 했을때마다 touchPower 받아옴.
			float _touchPower = Player.instance.touchPower;

			Vector2 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Ray2D ray = new Ray2D (pos, Vector2.zero);

			RaycastHit2D hit = Physics2D.Raycast(ray.origin,ray.direction);

			if (hit.transform == null) 
				return;

			touchTarget = hit.transform.gameObject;

			//EnemyScale 줄임.
			if (hit.collider.transform.tag.Equals("Enemy")) 
			{		
				touchTarget.transform.localScale = new Vector3 (touchTarget.transform.localScale.x - _touchPower,
																touchTarget.transform.localScale.y - _touchPower,
																touchTarget.transform.localScale.z);

				Player.instance.RotateSpeedUp ();
			}
		}
	}

	void GameTime()
	{
		//게임시간 흐름(점수)
		gameTime += Time.deltaTime;

		//생성쿨타임을 줄여줌.
		if(EnemyManager.instance.renderTime >= 0.5f)
			EnemyManager.instance.renderTime -= 0.00001f;

		if (EnemyManager.instance.maxScale <= 5f)
			EnemyManager.instance.maxScale += 0.0001f;
	}
}
