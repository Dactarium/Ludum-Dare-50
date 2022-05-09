using System;
using UnityEngine;

public class GetSoulFromTarget : MainEvent
{
    public override event Action<GameEvent> OnComplete;

    public Transform Target {get; private set;}

    private float _timeGain;

    private NpcSpawner _npcSpawner;
    private GameObject _deathMark;

    public GetSoulFromTarget(){
        LuckPoint = ConfigManager.Instance.GetSoulFromTarget_LuckPoint;

        _npcSpawner = ConfigManager.Instance.HumanSpawner;
        _timeGain = ConfigManager.Instance.GetSoulFromTarget_TimeGain;
    }

    public override void Initial()
    {
        Target = _npcSpawner.GetRandomNpc.transform;

        _deathMark = GameObject.Instantiate(ConfigManager.Instance.DeathMark);
        _deathMark.GetComponent<TargetFollower>().Target = Target;

        Target.GetComponent<Npc>().OnDeath += OnTargetDeath;

        
        string eventText = ConfigManager.Instance.GetSoulFromTarget_EventDetail;
        eventText = eventText.Replace("{name}", Target.name);

        UIManager.Instance.MainEventDetail = eventText;
        
        TargetPointer.Instance.gameObject.SetActive(true);
        TargetPointer.Instance.Target = Target;
    }

    public override void Execute()
    {
        
    }

    public override void Shutdown()
    {
       TargetPointer.Instance.gameObject.SetActive(false);
    }

    void OnTargetDeath(Npc npc){
        Target.GetComponent<Npc>().OnDeath -= OnTargetDeath;

        GameObject.Destroy(_deathMark);

        Timer.Instance.Add(_timeGain);

        NewsLine.Instance.EnqueueNews = DeathCausePicker.Instance.RandomCause(Target.name);

        GameEventManager.Instance.RandomMainEvent();

        OnComplete?.Invoke(this);
    }

}
