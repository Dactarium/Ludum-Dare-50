using System;
using UnityEngine;

public class DelayYourDeath : MainEvent
{
    public override event Action<GameEvent> OnComplete;

    private int _targetSoulAmount = 5;
    private int _soulCounter = 0;

    private float _timeGain;

    private GameObject _deathMark;

    public DelayYourDeath(){
        LuckPoint = ConfigManager.Instance.DelayYourDeath_LuckPoint;
        _timeGain = ConfigManager.Instance.DelayYourDeath_TimeGain;
        _targetSoulAmount = ConfigManager.Instance.DelayYourDeath_TargetSoulAmount;
    }

    public override void Initial(){
        Reaper.Instance.OnHumanCollect += OnCollectSoul;
        Reaper.Instance.OnLostCollect += OnCollectSoul;

        _deathMark = GameObject.Instantiate(ConfigManager.Instance.DeathMark);
        _deathMark.GetComponent<TargetFollower>().Target = ConfigManager.Instance.Player;

        SetEventDetail();
    }

    public override void Execute(){}

    public override void Shutdown(){
        _soulCounter = 0;

        Reaper.Instance.OnHumanCollect -= OnCollectSoul;
        Reaper.Instance.OnLostCollect -= OnCollectSoul;

        GameObject.Destroy(_deathMark);
    }

    void OnCollectSoul(Reaper reaper){
        _soulCounter++;

        SetEventDetail();

        if(_soulCounter < _targetSoulAmount) return;

        Timer.Instance.Add(_timeGain);

        GameEventManager.Instance.NextMainEvent(GameEventManager.GetSoulFromTarget);

        OnComplete?.Invoke(this);
    }

    void SetEventDetail(){
        string eventDetail = ConfigManager.Instance.DelayYourDeath_EventDetail;
        eventDetail = eventDetail.Replace("{amount}", _soulCounter.ToString()).Replace("{targetAmount}", _targetSoulAmount.ToString());
        UIManager.Instance.MainEventDetail = eventDetail;
    }   
}
