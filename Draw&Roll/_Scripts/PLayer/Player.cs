using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Rigidbody2D rb;
    public CameraFollow cam;

    public static Player Instance;

    private void Awake() {
        string ballName = PlayerPrefs.GetString("SelectedPlayer", "PlayerBall1");
        Instance = GetComponent<Player>();
        GameObject player = Instantiate(Resources.Load<GameObject>("Player/" + ballName), this.transform.position, this.transform.rotation) as GameObject;

        if(cam != null)
            cam.player = player.transform;
        

        rb = player.GetComponent<Rigidbody2D>();
    }


    private void Start() {
        StartCoroutine(WiatForStart()); 
    }

    public virtual IEnumerator WiatForStart() {
        yield return new WaitForSeconds(2f);
        rb.bodyType = RigidbodyType2D.Dynamic;
    }


    public void PlayerDestroyed() {

        GameMenager.Instance.StartEndScreen();
        Destroy(gameObject);
    }
}
