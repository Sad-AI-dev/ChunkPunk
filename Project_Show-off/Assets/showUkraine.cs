using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showUkraine : MonoBehaviour
{

    [SerializeField] GameObject ukraineButton;

    public void Donate()
    {
        if (!ukraineButton.activeInHierarchy)
            ukraineButton.SetActive(true);
        else
            ukraineButton.SetActive(false);
    }
}
