using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Hittable : MonoBehaviour
{
    [SerializeField] int price = 0;
    [SerializeField] TMP_Text priceLabel;
    [SerializeField] UnityEvent onHit;

    Player buyer;
    bool bought = false;

    private void Start()
    {
        priceLabel.text = price.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Projectile proj)) {
            if (bought && proj.owner == buyer) { Activate(); }
            else {
                TryPurchase(proj.owner);
            }
        }
    }

    void TryPurchase(Player p)
    {
        if (CoinManager.instance.TryBuy(p, price)) {
            bought = true;
            buyer = p;
            Activate();
        }
    }

    void Activate()
    {
        onHit?.Invoke();
    }
}
