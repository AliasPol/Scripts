using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMenager : MonoBehaviour {

    public float deadzone = 100f;


    private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;

    private Vector2 startTouch, swipeDelta;


    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
    }

    void Update()
    {
        //tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;



        if (Input.touches.Length > 0) {

            if(Input.touches[0].phase == TouchPhase.Began) {
                tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;
                tap = true;
                startTouch = Input.mousePosition;
                Debug.Log("tap");
            }
            else if(Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled) {
                if(!swipeDown && !swipeLeft && !swipeRight && !swipeUp) {
                    //PlayerController.Instance.GetSwipe(Swipe.shoot);
                    
                }

                Reset();
            }

        }


        //Calculate swipe
        swipeDelta = Vector2.zero;
        if(startTouch != Vector2.zero) {
            if(Input.touches.Length != 0) {
                swipeDelta = Input.touches[0].position - startTouch;
            }
            
        }

        //Deadzone for swipe
        if(swipeDelta.magnitude > deadzone) {

            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if(Mathf.Abs(x) > Mathf.Abs(y)) {
                //Left or Right
                if(x < 0) {
                    swipeLeft = true;
                    Debug.Log("swipe Left");
                    PlayerController.Instance.GetSwipe(Swipe.swipeLeft);
                }
                else {
                    swipeRight = true;
                    Debug.Log("swipe Right");
                    PlayerController.Instance.GetSwipe(Swipe.swipeRight);
                }
            }
            else {
                // Up or Down
                if(y < 0) {
                    swipeDown = true;
                    Debug.Log("swipe Down");
                    PlayerController.Instance.GetSwipe(Swipe.swipeDown);
                }
                else {
                    swipeUp = true;
                    Debug.Log("swipe Up");
                    PlayerController.Instance.GetSwipe(Swipe.swipeUp);
                }
            }

            Reset();
        }
    }
}
