using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Playing State Settings", menuName = "ScriptableObjects/PlayingStateSettings", order = 1)]
public class PlayingStateSettings : ScriptableObject
{
    public float MaxTime;
    public float MaxTimeGain;
    public float MinTargetDistance;
    public float MaxTargetDistance;
    public int SoulToDelay;
}
