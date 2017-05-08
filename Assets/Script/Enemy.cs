using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public float moveSpeed = 0.5f;

	void Update () {
		if (GameManager.instance.curState == GameState.game) {
			MoveEnemy (moveSpeed);
			OffEnemy ();
		}
	}

	void MoveEnemy(float _moveSpeed)
	{
		this.transform.position = Vector3.MoveTowards (this.transform.position, Vector3.zero, _moveSpeed * Time.deltaTime);
	}

	//Enemy Off
	void OffEnemy()
	{
		if (this.transform.localScale.x <= 0.3f) {
			this.gameObject.SetActive (false);
			this.transform.localScale = new Vector3 (.4f, .4f, 1f);
		}
	}

	//enemy collider
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.transform.tag.Equals ("Player")) {
			this.gameObject.SetActive (false);
		}
	}
}
