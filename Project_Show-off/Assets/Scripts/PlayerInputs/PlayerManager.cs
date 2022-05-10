using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //--------------singleton-----------------
    private void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }
    }
    public static PlayerManager instance;

    //-----------player management-----------
    public readonly List<Player> players = new();
    int linkedPlayerCount = 0; //keeps track of how many of the players have been linked

    [Header("UI Settings")]
    public List<GameObject> playerUI = new();

    public void AddPlayer(Player player)
    {
        players.Add(player);
        players.Sort((Player a, Player b) => a.id.CompareTo(b.id));
        //add player to external
        GoalManager.instance.players.Add(player.transform);
        GameplayManager.instance.scores.Add(player, 0);
        CoinManager.instance.money.Add(player, 0);
    }

    public Player GetUnlinkedPlayer()
    {
        if (linkedPlayerCount < players.Count) {
            linkedPlayerCount++;
            return players[linkedPlayerCount - 1];
        }
        return null;
    }
}