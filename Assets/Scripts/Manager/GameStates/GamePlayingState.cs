using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;
public class GamePlayingState : GameBaseState
{
    private float _maxTime = 45f;
    private float _timer = 45f;
    private float _maxtimeGain = 33f;

    private float _minTargetDistance = 75f;
    private float _maxTargetDistance = 375f;

    private int _soulCounter = 0;
    private int _soulToDelay = 5;
    private int _soulDelayCounter = 5;

    private NpcDestroyer _npcDestroyer;
    private InputManager _inputManager;
    private AudioMixer _mixer;
    private bool _isContinue = false;
    private Transform _player;
    private ColorAdjustments _colorAdjustments;
    public override void BeginState(GameStateManager gameStateManager)
    {
        _player = gameStateManager.Player.transform;
        _mixer = gameStateManager.Mixer;

        _inputManager = InputManager.Instance; 
        _inputManager.enabled = true;
        UIManager.Instance.ShowOnPlayingUI(true);

        TargetManager.Instance.pickTarget();
        gameStateManager.TargetPointer.SetActive(true);

        _npcDestroyer = NpcDestroyer.Instance;
        _npcDestroyer.OnTargetDestroy +=  OnTargetDestroy;
        _inputManager.OnPause += OnPause;
        _inputManager.OnUnpause += OnUnpause;

        if(!gameStateManager.GlobalVolume.profile.TryGet(out _colorAdjustments)) throw new System.Exception(nameof(_colorAdjustments));

        UIManager.Instance.SoulCounter = "0 / <color=#00AAFF>" + GameStateManager.Instance.WinCondition + "</color>";
    }

    public override void UpdateState(GameStateManager gameStateManager)
    {
        if(_inputManager.Pause)return;

        if(_soulCounter == gameStateManager.WinCondition && !_isContinue) AskContinue();

        _timer -= Time.deltaTime;
        UIManager.Instance.Timer = Mathf.CeilToInt(_timer).ToString();

        if(_timer < 0){
            gameStateManager.SwitchState(gameStateManager.GameEndState);
        }
    }

    public override void EndState(GameStateManager gameStateManager)
    {   
        gameStateManager.Player.GetComponent<Animator>().enabled = false;
        _inputManager.enabled = false;
        UIManager.Instance.ShowOnPlayingUI(false);
        
        gameStateManager.TargetPointer.SetActive(false);

        _npcDestroyer.OnTargetDestroy -=  OnTargetDestroy;
        _inputManager.OnPause -= OnPause;
        _inputManager.OnUnpause -= OnUnpause;

        gameStateManager.TotalSoulCollected = _soulCounter;
    }

    void OnPause(InputManager inputManager){
        Time.timeScale = 0;
        UIManager.Instance.ShowOnPauseUI(true);
        _mixer.SetFloat("SFX", -80);
        InputManager.Instance.enabled = false;
    }

    void OnUnpause(InputManager inputManager){
        Time.timeScale = 1;
        UIManager.Instance.ShowOnPauseUI(false);
        _mixer.SetFloat("SFX", PlayerPrefs.GetFloat("SFX", 0));
        InputManager.Instance.enabled = true;

    }

    void AskContinue(){
        _isContinue = true;
        _inputManager.OnPause -= OnPause;
        _inputManager.OnUnpause += Continue;

        _inputManager.TriggerPause();

        Time.timeScale = 0;

        _colorAdjustments.saturation.Override(-80);

        UIManager.Instance.ShowOnConditionComplete(true);
    }

    void Continue(InputManager inputManager){
        _colorAdjustments.saturation.Override(-20);
        UIManager.Instance.ShowOnConditionComplete(false);
        _inputManager.OnPause += OnPause;
    }

    void OnTargetDestroy(NpcDestroyer npcDestroyer){

        if(TargetManager.Instance.IsPlayer){
            _soulDelayCounter--;
            UIManager.Instance.SoulToDelay = _soulDelayCounter.ToString();
        }

        if(TargetManager.Instance.IsPlayer && _soulDelayCounter > 0) return;

        float distance = TargetManager.Instance.InitialDistance;
        distance = (distance > _maxTargetDistance)? _maxTargetDistance : (distance < _minTargetDistance)? _minTargetDistance : distance;
        distance /= _maxTargetDistance;

        Debug.Log(_maxtimeGain * distance);

        _timer += _maxtimeGain * distance;

        if(_timer > _maxTime) _timer = _maxTime;


        if(TargetManager.Instance.IsPlayer){
            _soulDelayCounter = _soulToDelay;
            UIManager.Instance.SoulToDelay = _soulDelayCounter.ToString();
            UIManager.Instance.ShowSoulToDelay(false);

            TargetManager.Instance.pickTarget();
        }

        _soulCounter++; 
        string soulCounterText;
        if(_soulCounter < GameStateManager.Instance.WinCondition) soulCounterText = _soulCounter + " / <color=#00AAFF>" + GameStateManager.Instance.WinCondition + "</color>";
        else soulCounterText = "<color=#00AAFF>" + _soulCounter + "</color>";

        UIManager.Instance.SoulCounter = soulCounterText;
    }
}
