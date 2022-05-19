using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    //--singleton--
    private void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }
    }
    public static ScoreManager instance;

    public Dictionary<Player, float> playerScores = new();

    //-----------score management------------
    public void AddScore(Player player, float score)
    {
        if (playerScores.ContainsKey(player)) {
            playerScores[player] += score;
        }
    }

    public float GetScore(Player player)
    {
        if (playerScores.ContainsKey(player)) {
            return playerScores[player];
        }
        return default;
    }

    public List<float> GetScores()
    {
        return new List<float>(playerScores.Values);
    }

    public List<float> GetSortedScores()
    {
        List<float> scores = GetScores();
        scores.Sort((float a, float b) => -a.CompareTo(b));
        return scores;
    }
}