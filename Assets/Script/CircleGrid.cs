using UnityEngine;
using System.Collections;

public enum mDirection
{
    Left,
    Right,
    None
}

public class CircleGrid : MonoBehaviour
{
    public GameObject[] child;


    public mDirection curDirection = mDirection.None;

    float _x, _y;

    [SerializeField]
    float r = 3f;

    bool mState = false;

    Vector3 curPos;
    Vector3 startPos;

    float time = 0;

    void SetPos()
    {
        for (int i = 0; i < child.Length; i++)
        {
            _x = Mathf.Sin((360 * ((float)i / child.Length)) * Mathf.Deg2Rad) * r;
            _y = Mathf.Cos((360 * ((float)i / child.Length)) * Mathf.Deg2Rad) * r;

            child[i].transform.position = new Vector3(_x, _y, 0f);
            
        }
    }

    void TouchScreen()
    {
        if(Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
        }
        if(Input.GetMouseButton(0))
        {
            mState = true;
        }
        else
        {
            mState = false;
        }
    }

    void MoveChild()
    {
        if(mState)
        {
            curPos = Input.mousePosition;

            if((startPos.x - curPos.x) > 0)
            {
                curDirection = mDirection.Left;
            }
            else if ((startPos.x - curPos.x) < 0)
            {
                curDirection = mDirection.Right;
            }
        }
        else
        {
            curDirection = mDirection.None;
        }


        switch(curDirection)
        {

            case mDirection.Left:
                time += Time.deltaTime * 100f;

                for (int i = 0; i < child.Length; i++)
                {
                    _x = Mathf.Sin((360 * ((float)i / child.Length) - time) * Mathf.Deg2Rad ) * r;
                    _y = Mathf.Cos((360 * ((float)i / child.Length) - time) * Mathf.Deg2Rad ) * r;

                    child[i].transform.position = new Vector3(_x, _y, 0f);

                }
                break;
            case mDirection.Right:
                time += Time.deltaTime * 100f;

                for (int i = 0; i < child.Length; i++)
                {
                    _x = Mathf.Sin((360 * ((float)i / child.Length) + time) * Mathf.Deg2Rad) * r;
                    _y = Mathf.Cos((360 * ((float)i / child.Length) + time) * Mathf.Deg2Rad) * r;

                    child[i].transform.position = new Vector3(_x, _y, 0f);

                }
                break;
            case mDirection.None:
                break;

        }
    }

    private void Start()
    {
        SetPos();
    }

    private void Update()
    {
        TouchScreen();
        MoveChild();
    }

}
