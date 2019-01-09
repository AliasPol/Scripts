using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeMenu : MonoBehaviour {

    //Singleton
    private static FadeMenu instance;
    public static FadeMenu Instance {
        get
        {
            return instance;
        }
    }

    private Animator _animator;

    private void Awake() {
        instance = GetComponent<FadeMenu>();
        _animator = GetComponent<Animator>();
    }

    public void FadeMenuShow(string selectedLevel) {
        _animator.SetBool("IsOpen", true);

        StartCoroutine(WaitForAnimationEnd(selectedLevel));
    }

    IEnumerator WaitForAnimationEnd(string levelName) {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(levelName);
        FadeMenuClose();
    }


    public void FadeMenuClose() {
        _animator.SetBool("IsOpen", false);
    }

}
