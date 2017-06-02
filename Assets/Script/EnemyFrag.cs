using UnityEngine;
using System.Collections;

public class EnemyFrag : MonoBehaviour
{
    float delayTime = 0f;

    private void Update()
    {
        delayTime += Time.deltaTime;

        if (delayTime >= .6f)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, Vector3.zero, 10f * Time.deltaTime);
        }

        if(GameManager.instance.curState == GameState.over)
            Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag.Equals("Player"))
        {
            Player.instance.RotateSpeedUp();
            Destroy(this.gameObject);
        }
    }
}
