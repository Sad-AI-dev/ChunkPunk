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
    
    
    //----------------quit game-----------------
    public void QuitGame()
    {
        Application.Quit();
    }
}