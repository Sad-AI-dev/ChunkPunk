using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MakeButtonBigger : MonoBehaviour
{
    UnityEvent hoverPlay;

    void Start()
    {
        if (hoverPlay == null)
            hoverPlay = new UnityEvent();

        hoverPlay.AddListener(bigBoy);
    }


    private void bigBoy()
    {

    }
}
