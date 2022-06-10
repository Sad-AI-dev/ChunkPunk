using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public int maximumBullets;
    //--------singleton-------
    private void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }
    }
    public static CoinManager instance;

    //track money per player
    public Dictionary<Player, int> bullets = new();
    //UI
    readonly TMP_Text[] moneyLabels = new TMP_Text[2];

    private void Start()
    {
        foreach(Player player in PlayerManager.instance.players)
        {
            moneyLabels[PlayerManager.instance.players.IndexOf(player)].text = $"Bullets: {bullets[player]} / {maximumBullets}";
        }
        
        for (int i = 0; i < moneyLabels.Length; i++) {
            moneyLabels[i] = PlayerManager.instance.playerUI[i].ammoLabel;
        }
    }

    //--------------money management---------------
    public bool TryBuy(Player p, int amount)
    {
        if (CanPlayerAfford(p, amount)) {
            ChargeMoney(p, amount);
            return true;
        }
        return false;
    }
    
    public bool CanPlayerAfford(Player p, int amount)
    {
        return bullets[p] >= amount;
    }

    public void GainBullets(Player p, int amount)
    {
        if(this.bullets[p] < maximumBullets)
        {
            bullets[p] += amount;
            if (bullets[p] > maximumBullets)
                bullets[p] = maximumBullets;
            UpdateBulletsLevel(p);

        }
    }
    public void ChargeMoney(Player p, int amount)
    {
        bullets[p] -= amount;
        UpdateBulletsLevel(p);
    }

    //-------------------UI-------------------
    void UpdateBulletsLevel(Player p)
    {
        moneyLabels[PlayerManager.instance.players.IndexOf(p)].text = $"Bullets: {bullets[p]} / {maximumBullets}";
    }
}
