using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChangeTheFace : MonoBehaviour
{
    public enum Face { Dying, Normal, Stunned };
    Face playerFace;
    [SerializeField] Material Dying;
    [SerializeField] Material Normal;
    [SerializeField] Material Stunned;
    MeshRenderer thisOne;
    [HideInInspector] public UnityEvent DyingFace;
    [HideInInspector] public UnityEvent NormalFace;
    [HideInInspector] public UnityEvent StunnedFace;
    private void Awake()
    {
        if (DyingFace == null)
            DyingFace = new UnityEvent();
        if (NormalFace == null)
            NormalFace = new UnityEvent();
        if (StunnedFace == null)
            StunnedFace = new UnityEvent();

        DyingFace.AddListener(ChangeDyingFace);
        NormalFace.AddListener(ChangeNormalFace);
        StunnedFace.AddListener(ChangeStunnedFace);
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
        if (playerFace == Face.Normal)
        {
            thisOne.material = Normal;
        } else if (playerFace == Face.Dying)
        {
            thisOne.material = Dying;
        } else if (playerFace == Face.Stunned)
        {
            thisOne.material = Stunned;
        }
    }


    private void ChangeDyingFace()
    {
        UpdateFace(Face.Dying);
    }

    private void ChangeNormalFace()
    {
        UpdateFace(Face.Normal);
    }

    private void ChangeStunnedFace()
    {
        UpdateFace(Face.Stunned);
    }

}
