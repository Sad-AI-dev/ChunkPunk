using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hittable : MonoBehaviour
{
    [Header("timing")]
    [SerializeField] float canHitDelay = 1f;
    [SerializeField] float activateDelay = 1f;
    [Header("gameplay settings")]
    [SerializeField] int price = 0;
    [SerializeField] UnityEvent onHit;

    [Header("technical settings")]
    [SerializeField] GameObject[] numberPrefabs = new GameObject[10];
    [SerializeField] Transform[] numberCreatePoints;
    [SerializeField] float letterSize = 1f;
    [Range(0f, 0.5f)] [SerializeField] float sizeStep = 0.1f;

    Player buyer;

    //states
    private bool canHit;
    bool bought;

    private void Start()
    {
        canHit = true;
        bought = false;
        SetupPrice();
    }

    //------------------create price models----------------
    void SetupPrice()
    {
        //get vars
        List<int> digits = GetDigits();
        float halfSize = letterSize / 2f;
        //create numbers
        for (int i = 0; i < digits.Count; i++) {
            foreach (Transform t in numberCreatePoints) {
                Transform num = Instantiate(numberPrefabs[digits[i]], t).transform;
                float offset = (letterSize * i) - (halfSize * (digits.Count - 1));
                num.localPosition -= new Vector3(offset, 0, 0); //offset letter
            }
        }
        //resize createPoints
        float size = digits.Count * sizeStep;
        foreach (Transform point in numberCreatePoints) {
            point.localScale -= new Vector3(size, size, 0);
        }
    }
    List<int> GetDigits()
    {
        List<int> digits = new List<int>();
        int num = price;
        while (num > 0) {
            digits.Add(num % 10);
            num /= 10;
        }
        digits.Reverse(); //restore orders
        return digits;
    }

    //----------------------handle getting hit---------------------------
    private void OnCollisionEnter(Collision collision)
    {
        //yield return StartCoroutine(hitReactTimer(collision));
        if (canHit && collision.gameObject.CompareTag("Player")) {
            if (collision.transform.TryGetComponent(out Projectile proj)) {
                if (bought && proj.owner == buyer) {
                    StartCoroutine(Activate());
                }
                else {
                    TryPurchase(proj.owner);
                }
                //got hit, go on cooldown
                StartCoroutine(CanHitCo());
            }
        }
    }

    void TryPurchase(Player p)
    {
        if (CoinManager.instance.TryBuy(p, price)) {
            bought = true;
            buyer = p;
            StartCoroutine(Activate());
        }
    }

    IEnumerator Activate()
    {
        yield return new WaitForSeconds(activateDelay);
        onHit?.Invoke();
    }

    private IEnumerator CanHitCo()
    {
        canHit = false;
        yield return new WaitForSeconds(canHitDelay);
        canHit = true;
    }
}