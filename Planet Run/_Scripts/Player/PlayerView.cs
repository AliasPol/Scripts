using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour {

    public Animator _animator;

    private PlayerLandController alienController;

    private void Start()
    {
        alienController = FindObjectOfType<PlayerLandController>();
    }


    public void LunchSwipeAnimationDown()
    {
        _animator.Play("SwipeDown");
    }
    public void LunchSwipeAnimationUp()
    {
        _animator.Play("SwipeUp");

    }

    public void LunchSwipeAnimationLeft()
    {
        if (alienController != null && alienController.CheckPosition())
            return;

        _animator.Play("LeftSwipe");

    }

    public void LunchSwipeAnimationRight()
    {
        if (alienController != null && alienController.CheckPosition())
            return;

        _animator.Play("SwipeRight");

    }


}
