using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoBallsFollow : MonoBehaviour {

    public Transform player;
    public Transform player2;
    public float smoothSpeed = 0.125f;



    private void FixedUpdate() {

        if (this.transform.position.x < player.position.x) {
            Vector3 positionMove = this.transform.position;
            positionMove.x = player.position.x;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, positionMove, smoothSpeed);
            this.transform.position = smoothedPosition;

            GameMenager.Instance.CalculateHighScore(player.position.x);
        }
        else if (this.transform.position.x < player2.position.x) {
            Vector3 positionMove = this.transform.position;
            positionMove.x = player2.position.x;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, positionMove, smoothSpeed);
            this.transform.position = smoothedPosition;

            GameMenager.Instance.CalculateHighScore(player2.position.x);
        }
        else {
            Vector3 positionMove = this.transform.position;
            positionMove.x = positionMove.x + smoothSpeed - 0.025f;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, positionMove, smoothSpeed);
            this.transform.position = smoothedPosition;
        }

        


    }
}
