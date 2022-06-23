using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class waitForVideo : MonoBehaviour
{

    [SerializeField] private float waitTime;
    float currentTime;
    public bool skipped;

    private void Awake()
    {
        StartCoroutine(LoadSceneCo());
    }

    IEnumerator LoadSceneCo()
    {

        yield return new WaitForSecondsRealtime(0.1f);
        skipped = false;
        currentTime = 0f;
        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
        bool isDoneThis = false;
        while (!asyncOperation.isDone)
        {
            Debug.Log(currentTime);
            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                isDoneThis = true;
            }
            if ((currentTime > waitTime || skipped) && isDoneThis)
            {
                //go to next scene
                asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
    private void Update()
    {

        currentTime += Time.deltaTime;
    }
}
