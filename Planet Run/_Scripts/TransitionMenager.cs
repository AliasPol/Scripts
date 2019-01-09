using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TransitionMenager : MonoBehaviour {

    public PlayableDirector director;
    public bool landToSpace;

    private PlanetsInfoData plantesData;

    public void Awake()
    {
        if (landToSpace) {
            plantesData = Resources.Load("PlanetsInfo/" + RoundMenager.Instance.GetNextName()) as PlanetsInfoData;
        }
    }

    public void Start()
    {
        
        StartCoroutine(CheckEnd());
    }

    public IEnumerator CheckEnd()
    {
        while(director.state == PlayState.Playing) {
            yield return new WaitForEndOfFrame();
        }
        
        OnEnd();
    }

    private void OnEnd()
    {
        if (landToSpace) {
            RoundMenager.Instance.SetPlanetsData(plantesData);
            ChangeLandToSpace();
        }
        else {
            ChangeSpaceToLand();
        }
    }

    public void ChangeLandToSpace()
    {
        RoundMenager.Instance.ChangeStageToSpace();
    }

    public void ChangeSpaceToLand()
    {
        RoundMenager.Instance.ChangeStageToLand();
    }
}
