using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public Dictionary<Player, int> scores;
    [Tooltip("Minimum amount of times a player needs to 'win' to end the game")]
    [SerializeField] int winScore = 2;

    //-----------UI----------
    [Header("UI Settings")]
    [SerializeField] TMP_Text timerLabel;

    private void Start()
    {
        //initialize score dictionary
        foreach (Player p in PlayerManager.instance.players) {
            scores.Add(p, 0);
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
        switch (activeState) {
            case State.setup:
                break;

            case State.race:
                break;

            case State.done:
                break;
        }
    }

    //-------------------score management-----------------
    public void GainScore(Player reciever, int amount = 1)
    {
        if (scores.ContainsKey(reciever)) {
            scores[reciever] += amount;
        }
    }

    public bool HasPlayerWonCheck()
    {
        foreach (int score in scores.Values) {
            if (score >= winScore) return true;
        }
        return false;
    }
    public bool HasPlayerWonCheck(Player p)
    {
        return scores[p] >= winScore;
    }

    public Player GetWinningPlayer()
    {
        foreach (Player p in scores.Keys) {
            if (scores[p] >= winScore) return p;
        }
        return null;
    }
}
