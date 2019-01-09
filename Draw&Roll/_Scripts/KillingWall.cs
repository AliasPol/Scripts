using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillingWall : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;

            SceneManager.LoadScene(2);
        }
    }

    private void Start() {
        StartCoroutine(CheckPosition());
    }

    private IEnumerator CheckPosition() {
        while (true) {
            float x = Camera.main.transform.position.x;

            x = (gameObject.transform.position.x + 30) - x;

            if (x<0) {
                Destroy(gameObject);
            }
            yield return new WaitForSeconds(3f);
        }
    }

    

}


