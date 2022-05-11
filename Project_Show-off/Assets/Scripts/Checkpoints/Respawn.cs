using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    Player[] players;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        players = FindObjectsOfType<Player>();
    }
    public static Respawn instance;
}
    /*
    public void Died(int ID)
    {
        Transform spawnPoint = checkPointManager.instance.allPlayerCheckPoints[ID];

        Player[] players  = FindObjectsOfType<Player>();

        foreach(Player player in players)
        {
            if(player.id == ID)
            {
                player.transform.position = checkPointManager.instance.allPlayerCheckPoints[ID].position;
            }
        }
    }


    */

