using UnityEngine;
using System;
public class GamePlayingState : GameBaseState
{
    private float _timer = 45f;
    private float _timeGain = 10f;
    private float _maxTime = 45f;

    private int _soulCounter = 0;
    private int _soulToDelay = 5;
    private int _soulDelayCounter = 5;

    private NpcDestroyer _npcDestroyer;
    public override void BeginState(GameStateManager gameStateManager)
    {
        InputManager.Instance.enabled = true;
        UIManager.Instance.ShowOnPlayingUI(true);

        TargetManager.Instance.pickTarget();
        gameStateManager.TargetPointer.SetActive(true);

        _npcDestroyer = NpcDestroyer.Instance;
        _npcDestroyer.OnTargetDestory +=  TargetDestroyed;
    }

    public override void UpdateState(GameStateManager gameStateManager)
    {
        _timer -= Time.deltaTime;
        string timerText = Mathf.CeilToInt(_timer).ToString();
        UIManager.Instance.Timer = timerText;
        if(_timer < 0){
            gameStateManager.SwitchState(gameStateManager.GameEndState);
        }
    }

    public override void EndState(GameStateManager gameStateManager)
    {   
        gameStateManager.Player.GetComponent<Animator>().enabled = false;
        InputManager.Instance.enabled = false;
        UIManager.Instance.ShowOnPlayingUI(false);
        
        gameStateManager.TargetPointer.SetActive(false);

        _npcDestroyer.OnTargetDestory -=  TargetDestroyed;

        gameStateManager.TotalSoulCollected = _soulCounter;
    }

    void TargetDestroyed(NpcDestroyer npcDestroyer){

        if(TargetManager.Instance.IsPlayer){
            _soulDelayCounter--;
            Debug.Log(_soulDelayCounter);
            UIManager.Instance.SoulToDelay = _soulDelayCounter.ToString();
        }

        if(TargetManager.Instance.IsPlayer && _soulDelayCounter > 0) return;

        _timer += _timeGain;
        if(_timer > _maxTime) _timer = _maxTime;

        if(TargetManager.Instance.IsPlayer){
            _soulDelayCounter = _soulToDelay;
            UIManager.Instance.SoulToDelay = _soulDelayCounter.ToString();
            UIManager.Instance.ShowSoulToDelay(false);

            TargetManager.Instance.pickTarget();
        }
        
        _soulCounter++; 

        UIManager.Instance.SoulCounter = _soulCounter.ToString();
    }
}
