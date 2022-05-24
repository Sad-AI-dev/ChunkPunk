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

    float raceTime = 0;
    [HideInInspector] public Player winner;

    //-----------UI----------
    [Header("UI Settings")]
    [SerializeField] TMP_Text timerLabel;
    TMP_Text[] scoreLabels = new TMP_Text[2];
    [SerializeField] GameObject gameplayUI, gameFinishedUI;
    [SerializeField] ScoreBoard board;


    private void Start()
    {
        Time.timeScale = 1f;
        SetupUI();
        OnStateChanged();
    }
    void SetupUI()
    {
        gameplayUI.SetActive(true);
        gameFinishedUI.SetActive(false);
        //load score labels
        for (int i = 0; i < scoreLabels.Length; i++) {
            Transform scoreLabel = PlayerManager.instance.playerUI[i].transform.Find("Score"); //it's string based and I hate it, might change this later
            scoreLabels[i] = scoreLabel.GetComponent<TMP_Text>();
        }
    }

    private void Update()
    {
        switch (activeState) {
            case State.setup:
                timer -= Time.deltaTime;
                UpdateSetupTimer();
                break;
            case State.race:
                raceTime += Time.deltaTime;
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
                raceTime = 0f;
                break;

            case State.done:
                FinishGame();
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
    void FinishGame()
    {
        gameplayUI.SetActive(false);
        gameFinishedUI.SetActive(true);
        board.BuildBoard();
        //pause game
        Time.timeScale = 0f;
    }

    //-------------------score management-----------------
    public void GainScore(Player reciever, int amount = 1)
    {
        if (scores.ContainsKey(reciever)) {
            ScoreManager.instance.AddScore(reciever, raceTime);
            scores[reciever] += amount;
            UpdateScoreLabel(reciever);

            //detect if game is over
            if (HasPlayerWonCheck(reciever)) {
                winner = reciever;
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

    void UpdateScoreLabel(Player p)
    {
        scoreLabels[PlayerManager.instance.players.IndexOf(p)].text = $"Score: {scores[p]}";
    }
}
