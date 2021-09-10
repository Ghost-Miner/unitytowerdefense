using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveBlueprint
{
    // Changed and expanded copy of Wave.cs

    [Header("Unit 1")]
    public GameObject u1_prefab;
    public int u1_Count;
    public int u1_rate;

    [Header("Unit 2")]
    public GameObject u2_prefab;
    public int u2_Count;
    public int u2_rate;
    public int u2_spawnDelay;

    [Header("Unit 3")]
    public GameObject u3_prefab;
    public int u3_Count;
    public int u3_rate;
    public int u3_spawnDelay;

    [Header("Total number of units")]
    public int unityCount;
}