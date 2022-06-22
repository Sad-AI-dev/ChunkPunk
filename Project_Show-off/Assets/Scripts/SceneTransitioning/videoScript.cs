using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class videoScript : MonoBehaviour
{
    public bool VideoDone;
    [SerializeField] VideoPlayer originalVideo;

    private void Awake()
    {
        originalVideo.Prepare();
        originalVideo.playOnAwake = false;
    }

    public void StartVideo()
    {

        //AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        originalVideo.Play();
        StartCoroutine(ShowVideo());
    }

    private IEnumerator ShowVideo()
    {
        //announce when video is done
        double time = originalVideo.clip.length;
        yield return new WaitForSeconds((float)time);
        VideoDone = true;
    }
}
