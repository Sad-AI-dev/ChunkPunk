using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makePuddles : MonoBehaviour
{
    bool isSlip;
    [SerializeField] private BoxCollider PuddleSpot;
    [SerializeField] private PhysicMaterial yesWet;
    [SerializeField] private PhysicMaterial noWet;
    public void thePuddle()
    {
        Debug.Log("hitthepuddle");
        if (isSlip)
        {
            isSlip = false;
            PuddleSpot.material = noWet;
        } else if (!isSlip)
        {
            isSlip = true;
            PuddleSpot.material = yesWet;
        }
    }
}
