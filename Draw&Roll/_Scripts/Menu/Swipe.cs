using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour {


    public float maxTime;
    public float minSwipeDistance;

    private float startTime;
    private float endTime;

    private Vector3 startPos;
    private Vector3 endPos;

    private void Update() {
        
        if(Input.touchCount > 0) {

            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) {
                startTime = Time.time;
                startPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) {
                endTime = Time.time;
                endPos = touch.position;

                CheckSwipe();
            }

        }

    }


    private void CheckSwipe() {

        float swipeDistane = (endPos - startPos).magnitude;
        float swipeTime = endTime - startTime;

        if(swipeTime < maxTime && swipeDistane > minSwipeDistance) {
            DoSwipe(swipeDistane, swipeTime);
        }

    }

    private void DoSwipe(float swipeDistance, float swipeTime) {

        Vector2 distance = endPos - startPos;
        if(Mathf.Abs(distance.x) > Mathf.Abs(distance.y)) {
            Debug.Log("HORIZONTAL SWIPE");

            if(distance.x > 0) {
                Debug.Log("RIGHT SWIPE");

            }
            else if(distance.x < 0){
                Debug.Log("LEFT SWIPE");
            }
        }
        else if(Mathf.Abs(distance.x) < Mathf.Abs(distance.y)) {
            Debug.Log("VERTICAL SWIPE");

            if(distance.y > 0) {
                Debug.Log("UP SWIPE");
            }
            else if(distance.y < 0) {
                Debug.Log("DOWN SWIPE");
            }

        }

    }

}
