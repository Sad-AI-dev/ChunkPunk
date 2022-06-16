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
    RenderTexture text;
    [SerializeField] VideoPlayer originalVideo;
    private void Awake()
    {
        StartCoroutine(ShowVideo());
    }

    private IEnumerator ShowVideo()
    {
        double time = originalVideo.clip.length;
        Debug.Log(time);
        yield return new WaitForSeconds((float)time);
        VideoDone = true;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    
}
