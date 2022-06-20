using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using TMPro;

[RequireComponent(typeof(PlayerInputManager))]
public class SetupController : MonoBehaviour
{
    int joinedPlayers;

    [SerializeField] string joinedText = "JOINED!";

    [SerializeField] List<TMP_Text> joinLabels;
    [SerializeField] Button startButton;

    [SerializeField] List<GameObject> disableObjects;

    [SerializeField] UnityEvent onPlayerJoin;

    private void Start()
    {
        joinedPlayers = -1;
    }

    public void OnPlayerJoined(PlayerInput input)
    {
        joinedPlayers++;
        //register input reciever
        DontDestroyOnLoad(input.gameObject);
        InputReciever reciever = input.GetComponent<InputReciever>();
        reciever.id = joinedPlayers;
        //update ui
        UpdatePlayerUI();
        //events
        onPlayerJoin?.Invoke();
    }

    void UpdatePlayerUI()
    {
        joinLabels[joinedPlayers].text = joinedText;
        disableObjects[joinedPlayers].SetActive(false);
        if (joinedPlayers >= 1) { //max amount of players have joined, enable start button
            startButton.interactable = true;
        }
    }
}