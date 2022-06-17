using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public void LoadRelativeScene(int offset)
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + offset);
    }

    //------------load specific scene---------------
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadSceneAsync(sceneIndex);
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
    
    //----------------quit game-----------------
    public void QuitGame()
    {
        Application.Quit();
    }
}