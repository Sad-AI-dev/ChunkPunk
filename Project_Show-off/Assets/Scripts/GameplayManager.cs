using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class GameplayManager : MonoBehaviour
{
    public enum State { 
        setup,
        race,
        done
    }

    //-----------------singleton-----------------
    private void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }
    }
    public static GameplayManager instance;

    //---game state management---
    [HideInInspector] public State activeState;

    //----score management------
    public Dictionary<Player, int> scores = new();
    [Tooltip("Minimum amount of times a player needs to 'win' to end the game")]
    [SerializeField] int winScore = 2;
    [SerializeField] float setupTime = 5f;
    float timer = 0;

    //-----------UI----------
    [Header("UI Settings")]
    [SerializeField] TMP_Text timerLabel;
    [SerializeField] List<TMP_Text> scoreLabels = new();


    private void Start()
    {
        OnStateChanged();
    }

    private void Update()
    {
        switch (activeState) {
            case State.setup:
                timer -= Time.deltaTime;
                UpdateSetupTimer();
                break;
        }
    }

    //-----------------game state management--------------------
    public void SetGameState(State state)
    {
        activeState = state;
        OnStateChanged();
    }

    void OnStateChanged()
    {
        //state specific
        switch (activeState) {
            case State.setup:
                StartTimer();
                break;

            case State.race:
                Goal g = GoalManager.instance.SpawnGoal();
                g.onReachGoal.AddListener((Player p) => GainScore(p));
                break;

            case State.done:
                break;
        }
    }

    //------setup state
    void StartTimer()
    {
        timerLabel.gameObject.SetActive(true);
        timer = setupTime;
        StartCoroutine(SetupTimerCo());
    }

    IEnumerator SetupTimerCo()
    {
        yield return new WaitForSeconds(setupTime);
        //start race state
        timerLabel.gameObject.SetActive(false);
        SetGameState(State.race);
    }

    //------done state

    //-------------------score management-----------------
    public void GainScore(Player reciever, int amount = 1)
    {
        if (scores.ContainsKey(reciever)) {
            scores[reciever] += amount;

            //detect if game is over
            if (HasPlayerWonCheck(reciever)) {
                SetGameState(State.done); //player won, end game
            }
            else {
                SetGameState(State.setup); //player hasn't won, repeat cycle
            }
        }
    }

    bool HasPlayerWonCheck(Player p)
    {
        return scores[p] >= winScore;
    }

    //-------------------------UI----------------------------
    void UpdateSetupTimer()
    {
        timerLabel.text = FormatTimer();
    }

    string FormatTimer()
    {
        return Mathf.Ceil(timer).ToString();
    }
}
