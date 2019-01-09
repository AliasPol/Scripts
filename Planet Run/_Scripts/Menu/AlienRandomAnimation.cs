using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienRandomAnimation : MonoBehaviour {

    public string[] animationName;
    private Animator _animator;

    private int previusIndex = -1;

    private Quaternion startRotation;

	void Start () {
        _animator = GetComponent<Animator>();
        startRotation = gameObject.transform.localRotation;
        StartCoroutine(AnimationChange());
	}
	
	private IEnumerator AnimationChange()
    {
        while (true) {
            float rnd1 = Random.Range(5f, 9f);
            yield return new WaitForSeconds(rnd1);

            gameObject.transform.localRotation = startRotation;


            int rnd = Random.Range(0, animationName.Length);
            while(rnd == previusIndex) {
                rnd = Random.Range(0, animationName.Length);
            }
            previusIndex = rnd;
            _animator.Play(animationName[rnd]);

            float lenght = 1;
            if(rnd == 2) {
                lenght = 3f;
            }


            yield return new WaitForSeconds(lenght);
        }
    }
}
