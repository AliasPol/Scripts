using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoBalls : MonoBehaviour {

    private Rigidbody2D rb1;
    private Rigidbody2D rb2;
    public TwoBallsFollow cam;

    private void Awake() {
        string ballName = PlayerPrefs.GetString("SelectedPlayer", "PlayerBall1");
        GameObject player = Instantiate(Resources.Load<GameObject>("Player/" + ballName), (this.transform.position + (Vector3.up*4)), this.transform.rotation) as GameObject;
        GameObject player2 = Instantiate(Resources.Load<GameObject>("Player/" + "PlayerBall1"), this.transform.position, this.transform.rotation) as GameObject;
        
        cam.player = player.transform;
        cam.player2 = player2.transform;

        rb1 = player.GetComponent<Rigidbody2D>();
        rb2 = player2.GetComponent<Rigidbody2D>();
    }

    private void Start() {
        StartCoroutine(WiatForStart());
    }

    public virtual IEnumerator WiatForStart() {
        yield return new WaitForSeconds(2f);
        rb1.bodyType = RigidbodyType2D.Dynamic;
        rb2.bodyType = RigidbodyType2D.Dynamic;
    }
}
