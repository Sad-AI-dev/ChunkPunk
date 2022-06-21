using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class GameplayManager : MonoBehaviour
{
    public enum State { 
        start,
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

    //------------Timing-----------------
    [Header("Timings")]
    [SerializeField] float startTime = 3f;
    [SerializeField] float setupTime = 5f;
    [SerializeField] float countdownTime = 3f;
    float timer = 0;
    float raceTime = 0;

    [Header("Events")]
    [SerializeField] private UnityEvent onGameStart;

    //countdown event
    bool triggeredCountdown = false;
    [SerializeField] UnityEvent onStartCountdown;

    //determine winner score
    [HideInInspector] public Player winner;

    //-----------UI----------
    [Header("UI Settings")]
    [SerializeField] TMP_Text timerLabel;
    TMP_Text[] scoreLabels;

    //state labels
    TMP_Text[] stateLabels;
    CanvasGroup[] stateLabelGroups;
    [SerializeField] string trapLabel;
    [SerializeField] string raceLabel;
    [SerializeField] float fadeTime = 1f;
    float stateTimer;

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
        //get UI refs
        scoreLabels = new TMP_Text[2];
        stateLabels = new TMP_Text[2];
        stateLabelGroups = new CanvasGroup[2];
        for (int i = 0; i < scoreLabels.Length; i++) {
            UIInterfacer playerUI = PlayerManager.instance.playerUI[i];
            scoreLabels[i] = playerUI.scoreLabel;
            //state labels
            stateLabels[i] = playerUI.stateLabel;
            stateLabelGroups[i] = playerUI.stateGroup;
        }
    }

    private void Update()
    {
        UpdateTimers();
    }

    void UpdateTimers()
    {
        switch (activeState) {
            case State.start:
                timer -= Time.deltaTime;
                UpdateSetupTimer();
                break;

            case State.setup:
                timer -= Time.deltaTime;
                UpdateUI();
                break;

            case State.race:
                raceTime += Time.deltaTime;
                UpdateStateLabels();
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
            case State.start:
                StartDelay();
                break;

            case State.setup:
                StartTimer();
                break;

            case State.race:
                StartRace();
                break;

            case State.done:
                FinishGame();
                break;
        }
    }
    //--------start state
    private void StartDelay()
    {
        timer = startTime;
        timerLabel.gameObject.SetActive(true);
        SetStateLabels(""); //hide state labels
        onGameStart?.Invoke();
        StartCoroutine(StartDelayCo());
    }

    private IEnumerator StartDelayCo()
    {
        yield return null;
        List<Player> players = PlayerManager.instance.players;
        foreach (Player p in players) {
            p.isStunned = true;
        }
        yield return new WaitForSeconds(startTime);
        foreach (Player p in players) {
            p.isStunned = false;
            PlayerManager.instance.players[PlayerManager.instance.players.IndexOf(p)].stateController.Skate?.Invoke();
        }
        SetGameState(State.setup);
    }

    //------setup state
    void StartTimer()
    {
        triggeredCountdown = false;
        timer = setupTime;
        timerLabel.gameObject.SetActive(true);
        StartCoroutine(SetupTimerCo());
        //UI
        SetStateLabels(trapLabel);
    }

    IEnumerator SetupTimerCo()
    {
        yield return new WaitForSeconds(setupTime);
        //start race state
        timerLabel.gameObject.SetActive(false);
        SetGameState(State.race);
    }

    void CountdownCheck()
    {
        if (!triggeredCountdown && timer < countdownTime) {
            triggeredCountdown = true;
            onStartCountdown?.Invoke();
        }
    }

    //------race state
    void StartRace()
    {
        Goal g = GoalManager.instance.SpawnGoal();
        g.onReachGoal.AddListener((Player p) => GainScore(p));
        raceTime = 0f;
        //UI
        SetStateLabels(raceLabel);
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
    void UpdateUI()
    {
        UpdateSetupTimer();
        UpdateStateLabels();
        CountdownCheck();
    }

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

    void SetStateLabels(string s)
    {
        foreach (TMP_Text t in stateLabels) {
            t.text = s;
        }
        stateTimer = 0;
        foreach (CanvasGroup group in stateLabelGroups) { group.alpha = 1; }
    }

    void UpdateStateLabels()
    {
        stateTimer += Time.deltaTime;
        foreach (CanvasGroup group in stateLabelGroups) {
            group.alpha = 1f - (Mathf.Min(stateTimer, fadeTime) / fadeTime);
        }
    }
}