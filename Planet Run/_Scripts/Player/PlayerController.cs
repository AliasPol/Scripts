using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Swipe { swipeLeft, swipeRight, swipeUp, swipeDown, shoot }

public class PlayerController : MonoBehaviour {

    
    public enum PositionOnMap { leftUp, centerUp, rightUp, leftCenter, center, rightCenter, leftBottom, centerBottom, rightBottom}

    private static PlayerController instance;
    public static PlayerController Instance
    {
        get
        {
            if(instance == null) {
                instance = FindObjectOfType<PlayerController>();
            }
            return instance;
        }
    }

    public GameObject shootPrefab;
    public Transform shootTransform;

    protected PlayerView pView;

    public float swipeSpeed;
    public PositionOnMap currentPosition;

    private void Awake()
    {
        pView = GetComponent<PlayerView>();
        SetSound();
    }

    protected virtual void SetSound()
    {
        MenagerAudio.Instance.PlayPlayerSoundEffectLoop(Sound.PlayerEffectLoopList.JetEngineLoopV2);
    }

    protected virtual IEnumerator SoundChange()
    {
        MenagerAudio.Instance.PlayPlayerSoundEffectLoop(Sound.PlayerEffectLoopList.JetEngineWhenMoving);
        yield return new WaitForSeconds(0.4f);
        SetSound();
    }

    public virtual void GetSwipe(Swipe swipe)
    {
        Vector2 pos = transform.position;

        switch (swipe) {
            case Swipe.swipeDown:
                if (currentPosition == PositionOnMap.leftBottom ||
                    currentPosition == PositionOnMap.centerBottom ||
                    currentPosition == PositionOnMap.rightBottom) {

                    //You cannot swipeDown false swipe!!
                }
                else {
                    pos = transform.position;
                    pos.y -= 1;

                    StartCoroutine(MoveToPointY(pos));
                    pView.LunchSwipeAnimationDown();
                    MenagerAudio.Instance.PlayPlayerSoundEffect(Sound.PlayerEffectList.JetMovingUpDown);
                    StartCoroutine(SoundChange());
                }
                break;
            case Swipe.swipeUp:
                if (currentPosition == PositionOnMap.leftUp ||
                    currentPosition == PositionOnMap.centerUp ||
                    currentPosition == PositionOnMap.rightUp) {

                    //You cannot swipeUp false swipe!!
                }
                else {
                    pos = transform.position;
                    pos.y += 1;

                    pView.LunchSwipeAnimationUp();
                    StartCoroutine(MoveToPointY(pos));
                    MenagerAudio.Instance.PlayPlayerSoundEffect(Sound.PlayerEffectList.JetMovingUpDown);
                    StartCoroutine(SoundChange());

                }
                break;
            case Swipe.swipeLeft:
                if (currentPosition == PositionOnMap.leftBottom ||
                    currentPosition == PositionOnMap.leftCenter ||
                    currentPosition == PositionOnMap.leftUp) {

                    //You cannot swipeLeft false swipe!!
                }
                else {
                    pos = transform.position;
                    pos.x -= 1;

                    pView.LunchSwipeAnimationLeft();
                    StartCoroutine(MoveToPointX(pos));
                    MenagerAudio.Instance.PlayPlayerSoundEffect(Sound.PlayerEffectList.JetMovingLeftRight);
                    StartCoroutine(SoundChange());

                }
                break;
            case Swipe.swipeRight:
                if (currentPosition == PositionOnMap.rightBottom ||
                    currentPosition == PositionOnMap.rightCenter ||
                    currentPosition == PositionOnMap.rightUp) {

                    //You cannot swipeRight false swipe!!
                }
                else {
                    pos = transform.position;
                    pos.x += 1;

                    pView.LunchSwipeAnimationRight();
                    StartCoroutine(MoveToPointX(pos));
                    MenagerAudio.Instance.PlayPlayerSoundEffect(Sound.PlayerEffectList.JetMovingLeftRight);
                    StartCoroutine(SoundChange());

                }
                break;
            case Swipe.shoot:
                if(ShootCounter.ShootValue > 0) {
                    ShootCounter.ShootValue--;
                    Instantiate(shootPrefab, shootTransform.position, Quaternion.identity);
                    MenagerAudio.Instance.PlaySoundEffect(Sound.EffectList.SpaceGunV1);

                }
                break;

        }
        UpdateCurrentPosiotion(pos);
        
        
    }

    protected virtual void UpdateCurrentPosiotion(Vector2 pos)
    {
        if (pos.y == 1) {
            if (pos.x == -1) {
                currentPosition = PositionOnMap.leftUp;
            }
            else if (pos.x == 0) {
                currentPosition = PositionOnMap.centerUp;
            }
            else {
                currentPosition = PositionOnMap.rightUp;
            }
        }
        else if (pos.y == 0) {
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
        else {
            if (pos.x == -1) {
                currentPosition = PositionOnMap.leftBottom;
            }
            else if (pos.x == 0) {
                currentPosition = PositionOnMap.centerBottom;
            }
            else {
                currentPosition = PositionOnMap.rightBottom;
            }
        }
    }


    protected IEnumerator MoveToPointX(Vector2 end)
    {

        while (transform.position.x != end.x) {
            float step = swipeSpeed * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, end, step);

            yield return new WaitForEndOfFrame();
        }
    }
    protected virtual IEnumerator MoveToPointY(Vector2 end)
    {
        
        while (transform.position.y != end.y) {
            float step = swipeSpeed * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, end, step);

            yield return new WaitForEndOfFrame();

        }
        
    }



}
