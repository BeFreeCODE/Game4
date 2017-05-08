using UnityEngine;
using System.Collections;

public class LightMove : MonoBehaviour {
	float mTime = 0f;
	[SerializeField]
	float moveSpeed = .5f;
	Vector3 moveVec = Vector3.zero;

	void FixedUpdate () {
		CircularMotion ();
	}

	void CircularMotion()
	{
		mTime += Time.deltaTime;

		moveVec.x = Mathf.Sin (mTime * moveSpeed) * 25;
		moveVec.y = Mathf.Cos (mTime * moveSpeed) * 25;

		this.transform.position = moveVec;
	}
}
