using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform player;
    public float smoothSpeed = 0.125f;



    private void FixedUpdate() {

        if (this.transform.position.x < player.position.x) {
            Vector3 positionMove = this.transform.position;
            positionMove.x = player.position.x;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, positionMove, smoothSpeed);
            this.transform.position = smoothedPosition;

            GameMenager.Instance.CalculateHighScore(player.position.x);
        }
        else {
            Vector3 positionMove = this.transform.position;
            positionMove.x = positionMove.x + smoothSpeed - 0.025f;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, positionMove, smoothSpeed);
            this.transform.position = smoothedPosition;
        }


    }

 
}
