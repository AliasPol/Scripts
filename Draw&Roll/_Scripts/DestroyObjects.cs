using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyObjects : MonoBehaviour {



    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            GameMenager.Instance.StartEndScreen();
        }
        else {
            Destroy(collision.gameObject);
        }
    }
}
