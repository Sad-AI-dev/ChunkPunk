using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static void LoadNextScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //------------load specific scene---------------
    public static void LoadScene(int sceneIndex)
    {
        SceneManager.LoadSceneAsync(sceneIndex);
    }
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    //----------------quit game-----------------
    public static void QuitGame()
    {
        Application.Quit();
    }
}