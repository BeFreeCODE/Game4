using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public float moveSpeed = 0.5f;

    public bool isScaling = false;

    private Vector3 newScale;

    private float smoothScaleSpeed = 10.0f;

    public GameObject frag;

    [SerializeField]
    private MeshRenderer myMat;
    [SerializeField]
    private Material newMat;


    private void OnEnable()
    {
        isScaling = false;
    }

    void Update ()
    {
        if (GameManager.instance == null)
            return;

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
            OffEnemy();
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
        if (this.transform.localScale.x <= 0.5f && isScaling)
        {
            if (GameManager.instance.curState == GameState.over)
            {
                this.gameObject.SetActive(false);
                this.newScale = this.transform.localScale = new Vector3(.5f, .5f, .5f);
                return;
            }
            SoundManager.instance.PlayEffectSound(0);

            GameObject newFrag = Instantiate(frag);
            newFrag.transform.position = this.transform.position;

            this.gameObject.SetActive(false);
            this.newScale = this.transform.localScale = new Vector3(.5f, .5f, .5f);

            GameManager.instance.PlusScore();
            //combo Up
            GameManager.instance.PlusCombo();
        }

        if (this.transform.localScale.x < 1.5f)
        {
            if (myMat != null)
            {
                myMat.material = newMat;
            }
        }
	}

	//enemy collider
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.transform.tag.Equals ("Player"))
        {
            //플레이어 죽음.
            Player.instance.PlayerDead();

            GameManager.instance.curState = GameState.over;
            
            this.gameObject.SetActive (false);
		}
        if(col.transform.tag.Equals("Wave"))
        {
            GameObject newFrag = Instantiate(frag);
            newFrag.transform.position = this.transform.position;

            this.gameObject.SetActive(false);
            this.newScale = this.transform.localScale = new Vector3(.5f, .5f, .5f);

            //combo Up
            GameManager.instance.PlusCombo();

            GameManager.instance.PlusScore();

            //Make Gem
            GameObject newGem = Instantiate(GameManager.instance.gemObj);
            newGem.transform.position = this.transform.position;
            SoundManager.instance.PlayEffectSound(2);
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
