using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Video;
using UnityEngine.UI;

public class LogoScreen : MonoBehaviour
{

    public string scene_to_load, movie_to_play;
    public bool landscape;
    FullScreenMovieScalingMode scalingMode;
    AsyncOperation async;

    public GameObject background;

    void Awake()
    {
        DontDestroyOnLoad(this);
        scalingMode = landscape ? FullScreenMovieScalingMode.AspectFit : FullScreenMovieScalingMode.None;
    }

    void Start()
    {
        LoadGame();
    }

    void LoadGame()
    {
        async = SceneManager.LoadSceneAsync(scene_to_load);
        async.allowSceneActivation = false;
        StartCoroutine(PlayLogoAndLoadGame());
    }


    IEnumerator PlayLogoAndLoadGame()
    {
        Handheld.PlayFullScreenMovie(movie_to_play, Color.black, FullScreenMovieControlMode.Hidden, scalingMode);

        yield return new WaitForEndOfFrame();
        
        
        async.allowSceneActivation = true;
        Destroy(this);
    }
}
