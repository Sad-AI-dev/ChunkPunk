using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    [SerializeField] public int maximumBullets;
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
    public Dictionary<Player, int> money = new();
    //UI
    readonly TMP_Text[] moneyLabels = new TMP_Text[2];

    private void Start()
    {

        foreach(Player player in PlayerManager.instance.players)
        {

            moneyLabels[PlayerManager.instance.players.IndexOf(player)].text = $"Bullets: {money[player]} / {maximumBullets}";
        }
        
        for (int i = 0; i < moneyLabels.Length; i++) {
            GameObject moneyLabel = PlayerManager.instance.playerUI[i];
            moneyLabels[i] = moneyLabel.transform.Find("Money").GetComponent<TMP_Text>();
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
        return money[p] >= amount;
    }

    public void GainMoney(Player p, int amount)
    {
        if(this.money[p] < maximumBullets)
        {
            money[p] += amount;
            UpdateMoneyLabel(p);

        }
    }
    public void ChargeMoney(Player p, int amount)
    {
        money[p] -= amount;
        UpdateMoneyLabel(p);
    }

    //-------------------UI-------------------
    void UpdateMoneyLabel(Player p)
    {
        moneyLabels[PlayerManager.instance.players.IndexOf(p)].text = $"Bullets: {money[p]} / {maximumBullets}";
    }
}