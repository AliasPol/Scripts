using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour {


    private void OnTriggerEnter2D(Collider2D collision) {
        this.GetComponent<BoxCollider2D>().isTrigger = false;
        if (collision.gameObject.tag == "Player") {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            
            foreach(GameObject player in players) {
                player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                player.SetActive(false);
            }

            FinishScreen.Instance.FinishRound();
            GameMenager.Instance.StartEndScreen();
        }
        
    }
}
