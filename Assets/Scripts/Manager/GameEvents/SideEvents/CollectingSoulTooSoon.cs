using System;
using UnityEngine;

public class CollectingSoulTooSoon : SideEvent
{
    public override event Action<GameEvent> OnComplete;

    private float _timeGain;

    public CollectingSoulTooSoon(){
        Show = false;

        _timeGain = ConfigManager.Instance.CollectingSoulTooSoon_TimeGain;
    }

    public override void Initial()
    {
        Reaper.Instance.OnPersonCollect += PersonCollected;
    }

    public override void Execute()
    {

    }

    public override void Shutdown()
    {
        Reaper.Instance.OnPersonCollect -= PersonCollected;
    }

    void PersonCollected(Reaper reaper, Transform person){
        if(GameEventManager.Instance.CurrentMainEvent is GetSoulFromTarget && (GameEventManager.Instance.CurrentMainEvent as GetSoulFromTarget).Target == person) return;

        Timer.Instance.Add(_timeGain);
        
        GameObject shadow = GameObject.Instantiate<GameObject>(ConfigManager.Instance.CollectingSoulTooSoon_Spawn_Object, person.position, person.rotation);
    }
}
