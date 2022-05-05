using System;
using UnityEngine;

public class CollectLosts : SideEvent
{
    public override event Action<GameEvent> OnComplete;

    private float _timeGain;

    public CollectLosts(){
        Show = false;

        _timeGain = ConfigManager.Instance.CollectLosts_TimeGain;
    }

    public override void Initial()
    {
        Reaper.Instance.OnLostCollect += LostCollected;
    }

    public override void Execute()
    {

    }

    public override void Shutdown()
    {
        Reaper.Instance.OnLostCollect -= LostCollected;
    }

    void LostCollected(Reaper reaper){
        Timer.Instance.Add(_timeGain * reaper.LastCollectedLostBuff.ColletingTimeGainMultiplier);
    }
}
