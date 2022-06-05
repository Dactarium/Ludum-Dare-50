using System.Collections;
using UnityEngine;
public class GamePlayingState : GameBaseState
{
    private InputManager _inputManager;
    private Timer _timer;

    private float _startTime;

    private int _taskCounter;
    private bool _isTargetTaskCountReached = false;
    public override void BeginState(GameStateManager gameStateManager)
    {
        _inputManager = InputManager.Instance;
        _inputManager.enabled = true;
        _inputManager.OnPause += OnPause;
        _inputManager.OnUnpause += OnUnpause;
        _inputManager.CheckJoystick();

        UIManager.Instance.ShowOnPlayingUI(true);

        _startTime = gameStateManager.StartTime;
        _timer = Timer.Instance;
        _timer.Set(_startTime);

        _timer.OnZero += OnTimerZero;

        TaskCounter.Instance.TargetCount = gameStateManager.TargetTaskCount;
        TaskCounter.Instance.OnTargetCountReach += OnTargetTaskCountReach;

        GameEventManager.Instance.CurrentMainEvent = GameEventManager.GetSoulFromTarget;
        GameEventManager.Instance.CurrentMainEvent.OnComplete += OnEventComplete;
    }

    public override void UpdateState(GameStateManager gameStateManager)
    {

    }

    public override void EndState(GameStateManager gameStateManager)
    {
        _timer.OnZero -= OnTimerZero;

        _inputManager.OnPause -= OnPause;
        _inputManager.OnUnpause -= OnUnpause;

        TaskCounter.Instance.OnTargetCountReach -= OnTargetTaskCountReach;

        UIManager.Instance.ShowOnPlayingUI(false);

        gameStateManager.TotalTaskCompleted = TaskCounter.Instance.Counter;

        GameEventManager.Instance.SelectNullEvent();
    }

    void OnTimerZero(Timer timer) => GameStateManager.Instance.EndGame();

    void OnEventComplete(GameEvent gameEvent)
    {
        TaskCounter.Instance.Counter++;
    }

    void OnTargetTaskCountReach(TaskCounter taskCounter)
    {
        _isTargetTaskCountReached = true;
        _inputManager.TriggerPause();
    }

    void OnPause(InputManager inputManager)
    {
        if (_isTargetTaskCountReached)
        {
            UIManager.Instance.ShowOnConditionComplete(true);
        }
        else
        {
            UIManager.Instance.ShowOnPauseUI(true);
        }

        _inputManager.StartCoroutine(ChangeTimeScale(0));
    }

    void OnUnpause(InputManager inputManager)
    {
        if (_isTargetTaskCountReached)
        {
            UIManager.Instance.ShowOnConditionComplete(false);
        }
        else
        {
            UIManager.Instance.ShowOnPauseUI(false);
        }

        _inputManager.StartCoroutine(ChangeTimeScale(1));
    }

    IEnumerator ChangeTimeScale(float scale)
    {
        yield return new WaitForEndOfFrame();
        Time.timeScale = scale;
    }

}
