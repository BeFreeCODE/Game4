using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public float moveSpeed = 0.5f;

    public bool isScaling = false;
    private Vector3 newScale;
    private float smoothScaleSpeed = 10.0f;

    void Update ()
    {

        if (GameManager.instance.curState == GameState.game)
        {
			MoveEnemy (moveSpeed);
			OffEnemy ();
            SmoothScale();
        }
        else if(GameManager.instance.curState == GameState.over)
        {
            SetNewScale(this.transform.localScale.x);
            isScaling = true;
            SmoothScale();
        }
	}

	void MoveEnemy(float _moveSpeed)
	{
		this.transform.position = Vector3.MoveTowards (this.transform.position, 
                                                        Vector3.zero,
                                                        _moveSpeed * Time.deltaTime);
	}

	//Enemy Off
	void OffEnemy()
	{
		if (this.transform.localScale.x <= 0.5f)
        {
			this.gameObject.SetActive (false);
            this.newScale = this.transform.localScale = new Vector3 (.5f, .5f, .5f);

            Player.instance.RotateSpeedUp();
            GameManager.instance.PlusScore();
        }
	}

	//enemy collider
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.transform.tag.Equals ("Player"))
        {
            Player.instance.RotateSpeedDown();
			this.gameObject.SetActive (false);
		}
        if(col.transform.tag.Equals("Wave"))
        {
            this.gameObject.SetActive(false);
        }
	}

    void SmoothScale()
    {
        if (transform.localScale == newScale)
            isScaling = false;

        if(isScaling)
            transform.localScale = Vector3.Lerp(transform.localScale, newScale, smoothScaleSpeed * Time.deltaTime);
    }
    public void SetNewScale(float _newScale)
    {
        newScale = new Vector3 (transform.localScale.x - _newScale,
        transform.localScale.y - _newScale,
        transform.localScale.z - _newScale);
    }
}
