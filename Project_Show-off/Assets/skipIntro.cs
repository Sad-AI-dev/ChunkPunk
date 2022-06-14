using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class skipIntro : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("I woke up");
        var myAction = new InputAction(binding: "<Gamepad>/<button>");
        myAction.started += content => skip();
        myAction.Enable();

    }

    private void skip()
    {
        Debug.Log("SKippppppppppppppppppppp");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
