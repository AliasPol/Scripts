using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColiision : MonoBehaviour {

    public GameObject shipObject;


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == 8) {
            RoundMenager.Instance.GameOver();
            shipObject.SetActive(false);
            Debug.Log("HIIIII2");
        }
        else if(other.gameObject.layer == 11) {
            Debug.Log("PlayerCollision");
        }
    }
}
