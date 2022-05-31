using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

[RequireComponent(typeof(PlayerInputManager))]
public class SetupController : MonoBehaviour
{
    int joinedPlayers = -1;

    [SerializeField] List<TMP_Text> joinLabels;
    [SerializeField] Button startButton;

    public void OnPlayerJoined(PlayerInput input)
    {
        joinedPlayers++;
        //register input reciever
        DontDestroyOnLoad(input.gameObject);
        input.GetComponent<InputReciever>().id = joinedPlayers;
        //update ui
        UpdatePlayerUI();
    }

    void UpdatePlayerUI()
    {
        joinLabels[joinedPlayers].text = "Joined!";
        if (joinedPlayers >= 1) { //max amount of players have joined, enable start button
            startButton.interactable = true;
        }
    }
}