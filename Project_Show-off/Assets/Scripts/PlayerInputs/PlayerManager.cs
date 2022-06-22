using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    public List<UIInterfacer> playerUI = new();

    public List<MeshRenderer> playerFaces = new();

    List<InputReciever> recievers;

    private void Start()
    {
        TryLinkPlayers();
    }
    void TryLinkPlayers()
    {
        recievers = new List<InputReciever>(FindObjectsOfType<InputReciever>());
        if (recievers.Count > 0) {
            //disable input manager
            GetComponent<PlayerInputManager>().DisableJoining();
        }
    }

    public void AddPlayer(Player player)
    {
        players.Add(player);
        SetupPlayer(player);
        players.Sort((Player a, Player b) => a.id.CompareTo(b.id));
        if (GoalManager.instance != null) { //add player to external
            GameplayManager.instance.scores.Add(player, 0);
            ScoreManager.instance.playerScores.Add(player, 0);
            //CoinManager.instance.bullets.Add(player, 0);
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

    //----------------------remove links-------------------
    public void RemoveRecievers()
    {
        for (int i = recievers.Count - 1; i >= 0; i--) {
            recievers[i].Unlink();
        }
    }
}