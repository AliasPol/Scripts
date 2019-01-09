using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour {

    public float moveSpeed= 1.6f;
    public float zPosition;

    public bool destroy = false;
    public bool moveToPlayer = true;
    public Transform spawnPosition;

    private SpaceLessCutter sLC;

    private void Awake()
    {
        if (!destroy) {
            sLC = GetComponent<SpaceLessCutter>();
        }

    }

    private void Start()
    {
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (true) {
            moveSpeed = SpawnSystem.speed;
            float step = moveSpeed * Time.deltaTime;
            Vector3 pos = transform.position;
            if (moveToPlayer)
                pos.z -= step;
            else
                pos.z += step;

            transform.position = pos;

            yield return new WaitForEndOfFrame();

            if(pos.z <= zPosition) {
                if (!destroy) {
                    transform.position = spawnPosition.position;
                   // sLC.SwitchOn();
                }
                else {
                    SpawnSystem.spawn = true;
                    Destroy(gameObject);
                }
            }
        }
    }
}
