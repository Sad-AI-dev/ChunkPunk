using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PreLoading : MonoBehaviour
{

    [SerializeField] videoScript video;
    private void OnEnable()
    {
        startIt(3);
    }

    public void startIt(int sceneIndex)
    {
        StartCoroutine(LoadYourAsyncScene(sceneIndex));
    }
    IEnumerator LoadYourAsyncScene(int sceneIndex)
    {
        yield return new WaitForSeconds(0.1f);
        yield return null;

        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Level_Iteration1");
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
        Debug.Log("Pro :" + asyncOperation.progress);
        //When the load is still in progress, output the Text and progress bar
        bool isDoneThis = false;
        while (!asyncOperation.isDone)
        {
            
            //Output the current progress
            Debug.Log(video.VideoDone);
            Debug.Log(asyncOperation.progress);
            Debug.Log("video" + video.VideoDone);
            // Check if the load has finished
            if(asyncOperation.progress >= 0.9f)
            {
                isDoneThis = true;
            }
            Debug.Log("done" + isDoneThis);
            if (video.VideoDone && isDoneThis)
            {
                //Change the Text to show the Scene is ready
                //Wait to you press the space key to activate the Scene
                Debug.Log("Finsihed");
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }


}
