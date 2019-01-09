using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMove : MonoBehaviour {

    public float moveSpeed = 0.1f;

    public bool moveToPlayer = true;
    public bool notImportant = false;
    private void Start()
    {
        if (notImportant) {
            StartCoroutine(MoveSecond());
        }
        else {
            StartCoroutine(Move());
        }
        
    }

    private IEnumerator Move()
    {
        yield return new WaitForSeconds(2.5f);
        while (true) {
            float moveSpeed2 = SpawnSystem.speed;
            float step = moveSpeed * 10 * Time.deltaTime;
            Vector3 pos = transform.position;
            if (moveToPlayer)
                pos.z -= step;
            else
                pos.z += step;

            transform.position = pos;

            yield return new WaitForEndOfFrame();
        }
    }
    private IEnumerator MoveSecond()
    {
        while (true) {
            float moveSpeed2 = SpawnSystem.speed;
            float step = moveSpeed * moveSpeed2 * Time.deltaTime;
            Vector3 pos = transform.position;
            pos += pos * step * 0.001f;

            transform.position = pos;

            yield return new WaitForEndOfFrame();
        }
    }
}
