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

    public void AddPlayer(Player player)
    {
        players.Add(player);
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