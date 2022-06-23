using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class skipIntro : MonoBehaviour
{
    [SerializeField] waitForVideo targetLoader;

    private void Awake()
    {
        var myAction = new InputAction(binding: "<Gamepad>/<button>");
        myAction.started += content => skip();
        myAction.Enable();

    }

    private void skip()
    {
        targetLoader.skipped = true;
    }
}
