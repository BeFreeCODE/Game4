using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {
	public static EnemyManager instance;

	public GameObject enemyPrefab;

	private int enemyMaxNum = 150;

	private float curTime = 0f;
	public  float renderTime = 1.0f;
	private float _x, _y;

	public  float maxScale = 1.0f;
	private float scale = 0f;

	public List<GameObject> enemyList = new List<GameObject> ();

	void Awake()
	{
		if (instance == null)
			instance = this;
	}

	void Start () 
	{
		MakeEnemy ();
	}
	
	void Update ()
	{
		if(GameManager.instance.curState == GameState.game)
			RenderEnemy ();
	}

	//makeEneym & addlist
	void MakeEnemy()
	{
		for (int i = 0; i < enemyMaxNum; i++) 
		{
			GameObject newEnemy = Instantiate (enemyPrefab);
			newEnemy.transform.parent = this.transform;
			newEnemy.SetActive (false);

			enemyList.Add (newEnemy);
		}
	}

	//get Enemy from List
	GameObject GetEnemy()
	{
		foreach (GameObject getEnemy in enemyList) 
		{
			if (!getEnemy.activeInHierarchy)
				return getEnemy;
		}
		return null;
	}

	//render Enemy to Scene
	void RenderEnemy()
	{
		curTime += Time.deltaTime;

		if (curTime >= renderTime) 
		{
			GameObject renEnemy = GetEnemy ();

			//_x,_y random
			RandomPos ();

			renEnemy.transform.position = new Vector3 (_x, _y, 0f);
			renEnemy.transform.localScale = new Vector3 (scale, scale, 1f);

			if (scale <= 0.5f) {
				renEnemy.GetComponent<Enemy> ().moveSpeed = 0.8f;
			} else if (scale <= 1.0f) {
				renEnemy.GetComponent<Enemy> ().moveSpeed = 0.6f;
			} else if (scale <= 1.5f) {
				renEnemy.GetComponent<Enemy> ().moveSpeed = 0.4f;	
			} else {
				renEnemy.GetComponent<Enemy> ().moveSpeed = 0.2f;
			}

			renEnemy.SetActive (true);

			curTime = 0f;
		}
	}

	void RandomPos()
	{
		_x = 0f;
		_y = 0f;

		scale = Random.Range (0.4f, maxScale);

		while (Mathf.Abs (_x) <= 3f && Mathf.Abs(_y) <= 3f) 
		{
			_x = Random.Range (-3f, 3f);
			_y = Random.Range (-5f, 5f);
		}
	}
}
