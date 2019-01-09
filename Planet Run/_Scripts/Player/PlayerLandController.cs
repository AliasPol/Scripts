using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandController : PlayerController {

    private bool canBeSlideUpOrDown = true;
    public bool jump = false;
    public bool slide = false;
    private bool runing = true;
    public Sound.PlayerEffectLoopList[] playerFootStep;

    private List<Coroutine> moveY = new List<Coroutine>();
    private List<Coroutine> moveX = new List<Coroutine>();

    private bool returnVolume = false;
    protected override void SetSound()
    {
        StartCoroutine(FootSteps());
    }

    private IEnumerator FootSteps()
    {
        while (true) {
            if (returnVolume) {
                MenagerAudio.Instance.TurnOnVolume(Sound.MixGroupsName.PlayerEffectsLoop);
                returnVolume = false;
            }

            if (runing) {
                int rnd = Random.Range(0, playerFootStep.Length);
                MenagerAudio.Instance.PlayPlayerSoundEffectLoop(playerFootStep[rnd]);

            }
            else {
                MenagerAudio.Instance.TurnOffVolume(Sound.MixGroupsName.PlayerEffectsLoop);
                runing = true;
                if(slide || jump) {
                    yield return new WaitForSeconds(0.43f);
                }
                returnVolume = true;
            }
            yield return new WaitForSeconds(0.23f);
        }
    }

    public override void GetSwipe(Swipe swipe)
    {

        Vector2 pos = transform.position;

        switch (swipe) {
            case Swipe.swipeDown:
                if ((currentPosition == PositionOnMap.leftCenter ||
                    currentPosition == PositionOnMap.center ||
                    currentPosition == PositionOnMap.rightCenter) && !slide) {

                    canBeSlideUpOrDown = true;

                    SlideDown();
                    pView.LunchSwipeAnimationDown();
                    MenagerAudio.Instance.PlayPlayerSoundEffect(Sound.PlayerEffectList.AlienSlidev2);
                    runing = false;
                }
                else {
                    //You cannot swipeDown false swipe!!
                    
                }
                break;
            case Swipe.swipeUp:
                Debug.Log(canBeSlideUpOrDown);
                if (/*(currentPosition == PositionOnMap.leftCenter ||
                    currentPosition == PositionOnMap.center ||
                    currentPosition == PositionOnMap.rightCenter) &&*/ !jump) {

                    canBeSlideUpOrDown = true;

                    pView.LunchSwipeAnimationUp();
                    Jump();
                    runing = false;
                    MenagerAudio.Instance.PlayPlayerSoundEffect(Sound.PlayerEffectList.AlienJumpv2);

                }
                else {
                    
                    //You cannot swipeUp false swipe!!
                    
                }
                break;
            case Swipe.swipeLeft:
                if (currentPosition == PositionOnMap.leftBottom ||
                    currentPosition == PositionOnMap.leftCenter ||
                    currentPosition == PositionOnMap.leftUp) {

                    //You cannot swipeLeft false swipe!!
                }
                else if (runing){
                    runing = false;
                    pos = transform.position;
                    pos.x = XPositionForSwipe();
                    pos.x -= 1;

                    pView.LunchSwipeAnimationLeft();

                    foreach (Coroutine z in moveX) {
                        StopCoroutine(z);
                    }
                    moveX.Clear();

                    moveX.Add(StartCoroutine(MoveToPointX(pos)));
                    MenagerAudio.Instance.PlayPlayerSoundEffect(Sound.PlayerEffectList.AlienMovev3);
                    
                }
                break;
            case Swipe.swipeRight:
                if (currentPosition == PositionOnMap.rightBottom ||
                    currentPosition == PositionOnMap.rightCenter ||
                    currentPosition == PositionOnMap.rightUp) {

                    //You cannot swipeRight false swipe!!
                }
                else  if(runing){
                    runing = false;
                    pos = transform.position;
                    pos.x = XPositionForSwipe();
                    pos.x += 1;

                    pView.LunchSwipeAnimationRight();

                    foreach(Coroutine z in moveX) {
                        StopCoroutine(z);
                    }
                    moveX.Clear();
                    moveX.Add(StartCoroutine(MoveToPointX(pos)));
                    MenagerAudio.Instance.PlayPlayerSoundEffect(Sound.PlayerEffectList.AlienMovev4);
                    

                }
                break;
            case Swipe.shoot:
                if (ShootCounter.ShootValue > 0) {
                    ShootCounter.ShootValue--;
                    MenagerAudio.Instance.PlaySoundEffect(Sound.EffectList.SpaceGunV2);
                    Instantiate(shootPrefab, shootTransform.position, Quaternion.identity);
                    
                }
                break;

        }

        UpdateCurrentPosiotion(pos);
    }

    private float XPositionForSwipe()
    {
        if(currentPosition == PositionOnMap.leftCenter) {
            return -1;
        }
        else if (currentPosition == PositionOnMap.center) {
            
            return 0;
        }
        else {
            return 1;
        }
    }

    protected override void UpdateCurrentPosiotion(Vector2 pos)
    {


        if (pos.x == -1) {
            currentPosition = PositionOnMap.leftCenter;
        }
        else if (pos.x == 0) {
            currentPosition = PositionOnMap.center;
        }
        else {
            currentPosition = PositionOnMap.rightCenter;
        }
    }

    private void SlideDown()
    {

        slide = true;
        jump = false;
        Vector3 posStart = Vector3.zero;
        posStart.y = -0.7f;

        Vector3 position = transform.position;
        position.y = 0;
        transform.position = position;
        /*
        Vector2 pos = transform.position;
        pos.y -= 0.7f;*/

        
        foreach(Coroutine z in moveY) {
            StopCoroutine(z);
        }
        moveY.Clear();
        
        moveY.Add(StartCoroutine(MoveToPointY(posStart)));
    }

    private void Jump()
    {
        jump = true;
        slide = false;
        Vector3 posStart = Vector3.zero;
        posStart.y = 1.75f;

        Vector3 position = transform.position;
        position.y = 0;
        transform.position = position;
        /*
        Vector2 pos = transform.position;
        pos.y += 1.75f;*/


        foreach (Coroutine z in moveY) {
            StopCoroutine(z);
        }
        moveY.Clear();
        moveY.Add(StartCoroutine(MoveToPointY(posStart)));
    }

    protected override IEnumerator MoveToPointY(Vector2 end)
    {

        Vector2 pos = transform.position;
        pos.y = 0;

        while (transform.position.y != end.y) {
            Vector2 posToEnd = transform.position;
            posToEnd.y = end.y;

            float step = swipeSpeed * Time.deltaTime;

            Vector2 newPos = Vector2.MoveTowards(transform.position, posToEnd, step);


            transform.localPosition = newPos;

            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(0.5f);

        while (transform.position.y != pos.y) {
            Vector2 posToEnd = transform.position;
            posToEnd.y = pos.y;
            float step = swipeSpeed * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, posToEnd, step);

            yield return new WaitForEndOfFrame();
        }
        canBeSlideUpOrDown = true;


        if(pos.y > end.y) {
            slide = false;
        }
        else {
            jump = false;
        }

    }

    public bool CheckPosition()
    {
        if (jump || slide)
            return true;
        else
            return false;
    }
}
