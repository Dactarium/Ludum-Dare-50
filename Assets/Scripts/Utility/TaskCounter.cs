using System;
using UnityEngine;

public class TaskCounter : MonoBehaviour
{
    public static TaskCounter Instance {get; private set;}

    public event Action<TaskCounter> OnTargetCountReach;
    public int TargetCount{
        set{
            _targetCount = value;
            Counter = _counter;
        }
        get{
            return _targetCount;
        }
    }
    private int _targetCount = 0;
    
    public int Counter{
        set{
            _counter = value;
            if(_counter < _targetCount) UIManager.Instance.TaskCounter = _counter + " / <color=#00AAFF>" + _targetCount + "</color>";
            else if(_counter == _targetCount){
                UIManager.Instance.TaskCounter = "<color=#00AAFF>" + _counter + " / " + _targetCount + "</color>";
                OnTargetCountReach?.Invoke(this);
            }else UIManager.Instance.TaskCounter = "<color=#00AAFF>" + _counter + "</color>";
        }
        get{
            return _counter;
        }
    }

    private int _counter = 0;

    void Awake(){
        Instance = this;
    }
}
