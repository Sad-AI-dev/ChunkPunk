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

    [Header("Camera aim Settings")]
    [SerializeField] List<GameObject> neutralVCams = new();
    [SerializeField] List<GameObject> AimVCams = new();

    [Header("UI Settings")]
    public List<GameObject> playerUI = new();

    public void AddPlayer(Player player)
    {
        players.Add(player);
        SetupPlayer(player);
        players.Sort((Player a, Player b) => a.id.CompareTo(b.id));
        if (GoalManager.instance != null) { //add player to external
            GameplayManager.instance.scores.Add(player, 0);
            ScoreManager.instance.playerScores.Add(player, 0);
            GoalManager.instance.players.Add(player.transform);
            CoinManager.instance.money.Add(player, 0);
        }
    }

    void SetupPlayer(Player player)
    {
        int index = player.id - 1;
        player.neutralVCam = neutralVCams[index];
        player.aimVCam = AimVCams[index];
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