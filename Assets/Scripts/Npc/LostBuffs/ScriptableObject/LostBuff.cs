using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Buff", menuName = "ScriptableObjects/Buffs/LostBuff", order = 0)]
public class LostBuff : ScriptableObject
{
    [Header("Lost Settings")]
    public Material Material;
    [Range(0.5f, 10f)]public float ColletingTimeGainMultiplier = 1f;

    [Header("Buff Settings")]
    [Range(0, 100)] public int LuckPoint = 1;
    [Range(0, 2)] public float InOutDelay = 0f;
    [Range(0, 15)] public float Duration = 0f;
    [Range(0.1f, 2)] public float SpeedMultiplier = 1f;
    public bool DoubleTimeGain = false;
}
