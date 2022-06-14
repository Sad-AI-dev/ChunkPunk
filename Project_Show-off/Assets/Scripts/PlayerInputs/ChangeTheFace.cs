using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTheFace : MonoBehaviour
{
    public enum Face { Dying, Normal, Stunned };
    Face playerFace;
    [SerializeField] Material Dying;
    [SerializeField] Material Normal;
    [SerializeField] Material Stunned;
    MeshRenderer thisOne;
    private void Awake()
    {
        playerFace = Face.Normal;
        thisOne = GetComponent<MeshRenderer>();
    }

    public void UpdateFace(Face current)
    {
        if (current == Face.Dying || current == Face.Normal || current == Face.Stunned) {
            {
                playerFace = current;
            }
        }
        
    }

    private void Update()
    {
        if(playerFace == Face.Normal)
        {
            thisOne.material = Normal;
        } else if (playerFace == Face.Dying)
        {
            thisOne.material = Dying;
        } else if(playerFace == Face.Stunned)
        {
            thisOne.material = Stunned;
        }
    }

}
