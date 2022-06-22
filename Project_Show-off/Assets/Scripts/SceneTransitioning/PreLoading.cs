using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PreLoading : MonoBehaviour
{
    [SerializeField] videoScript video;
    [HideInInspector] public bool skipped;

    public void LoadNextSceneInBackground()
    {
        skipped = false;
        StartCoroutine(LoadSceneCo());
    }
    IEnumerator LoadSceneCo()
    {
        yield return new WaitForSecondsRealtime(0.1f);

        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 2);
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
        bool isDoneThis = false;
        while (!asyncOperation.isDone) {
            // Check if the load has finished
            if(asyncOperation.progress >= 0.9f) {
                isDoneThis = true;
            }
            if ((video.VideoDone || skipped) && isDoneThis) {
                //go to next scene
                asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }


}
