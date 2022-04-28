using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    private void Start()
    {
        GoalManager.instance.players.Add(transform);
    }
}
