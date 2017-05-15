using UnityEngine;
using System.Collections;

public class EnemyMesh : MonoBehaviour {
    private float rotSpeed = 3f;

    void RotateEnemy(float _rotSpeed)
    {
        this.transform.Rotate(new Vector3(Random.Range(10, 50),
                                           Random.Range(10, 50),
                                           Random.Range(10, 50))
                                           * Time.deltaTime
                                           * _rotSpeed, Space.Self);
    }

    void Update () {
        RotateEnemy(rotSpeed);
    }
}
