using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;
public class GamePlayingState : GameBaseState
{
    #region Variables
    private float _maxTime ; 
    private float _timer;
    private float _maxtimeGain;

    private float _minTargetDistance;
    private float _maxTargetDistance;

    
    private int _soulToDelay;
    private int _soulDelayCounter;

    private int _soulCounter = 0;

    private int _winCondition;

    private NpcDestroyer _npcDestroyer;
    private InputManager _inputManager;
    private AudioMixer _mixer;
    private ColorAdjustments _colorAdjustments;
    #endregion
    
    public override void BeginState(GameStateManager gameStateManager)
    {
        #region Assign variables
        _mixer = gameStateManager.Mixer;

        _inputManager = InputManager.Instance; 
        _npcDestroyer = NpcDestroyer.Instance;

        _maxTime = gameStateManager.playingStateSettings.MaxTime;
        _timer = _maxTime;

        _maxtimeGain = gameStateManager.playingStateSettings.MaxTimeGain;

        _minTargetDistance = gameStateManager.playingStateSettings.MinTargetDistance;
        _maxTargetDistance = gameStateManager.playingStateSettings.MaxTargetDistance;

        _soulToDelay = gameStateManager.playingStateSettings.SoulToDelay;
        _soulDelayCounter = _soulToDelay;

        _winCondition = gameStateManager.WinCondition;
        #endregion

        #region Enable objects
        _inputManager.enabled = true;
        #endregion

        #region Pick target and enable pointer
        TargetManager.Instance.pickTarget();
        gameStateManager.TargetPointer.SetActive(true);
        #endregion
        
        #region Assign event listeners
        _npcDestroyer.OnTargetDestroy +=  OnTargetDestroy;
        _inputManager.OnPause += OnPause;
        _inputManager.OnUnpause += OnUnpause;
        #endregion

        #region Update globalvolume
        if(!gameStateManager.GlobalVolume.profile.TryGet(out _colorAdjustments)) throw new System.Exception(nameof(_colorAdjustments));
        #endregion
        
        #region Show UI and Set initial values of UI
        UIManager.Instance.ShowOnPlayingUI(true);
        UIManager.Instance.SoulCounter = "0 / <color=#00AAFF>" + GameStateManager.Instance.WinCondition + "</color>";
        #endregion
    
    }

    public override void UpdateState(GameStateManager gameStateManager)
    {
        if(_inputManager.Pause)return;
        
        #region Timer
        _timer -= Time.deltaTime;
        UIManager.Instance.Timer = Mathf.CeilToInt(_timer).ToString();

        if(_timer < 0){
            gameStateManager.SwitchState(gameStateManager.GameEndState);
        }
        #endregion
    }

    public override void EndState(GameStateManager gameStateManager)
    {   
        UIManager.Instance.ShowOnPlayingUI(false);
        
        gameStateManager.TargetPointer.SetActive(false);

        _npcDestroyer.OnTargetDestroy -=  OnTargetDestroy;
        _inputManager.OnPause -= OnPause;
        _inputManager.OnUnpause -= OnUnpause;

        gameStateManager.TotalSoulCollected = _soulCounter;
    }

    void OnPause(InputManager inputManager){
        Time.timeScale = 0;

        _mixer.SetFloat("SFX", -80);

        _colorAdjustments.saturation.Override(-80);

        if(_soulCounter == _winCondition) UIManager.Instance.ShowOnConditionComplete(true);
        else UIManager.Instance.ShowOnPauseUI(true);

    }

    void OnUnpause(InputManager inputManager){
        Time.timeScale = 1;

        _mixer.SetFloat("SFX", PlayerPrefs.GetFloat("SFX", 0));

        _colorAdjustments.saturation.Override(-20);

        if(_soulCounter == _winCondition) UIManager.Instance.ShowOnConditionComplete(false);
        else UIManager.Instance.ShowOnPauseUI(false);
        
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
        if(_soulCounter < _winCondition) soulCounterText = _soulCounter + " / <color=#00AAFF>" + GameStateManager.Instance.WinCondition + "</color>";
        else soulCounterText = "<color=#00AAFF>" + _soulCounter + "</color>";

        UIManager.Instance.SoulCounter = soulCounterText;

        #region Check win condition
        if(_soulCounter == _winCondition){
            _inputManager.TriggerPause();
        }
        #endregion
    }
}
