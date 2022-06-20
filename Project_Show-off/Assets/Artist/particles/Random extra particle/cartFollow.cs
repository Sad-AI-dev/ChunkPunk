using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cartFollow : MonoBehaviour
{
    [SerializeField] GameObject Cart;

    // Update is called once per frame
    void Update()
    {
        transform.position = Cart.transform.position;
    }
}
