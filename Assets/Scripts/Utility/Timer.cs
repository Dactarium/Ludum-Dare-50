using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer Instance{get; private set;}

    public event Action<Timer> OnZero;
    
    private float _time = -1;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if(InputManager.Instance.Pause)return;

        if(_time > 0){
            _time -= Time.deltaTime;
    
            UIManager.Instance.Timer = Mathf.CeilToInt(_time).ToString();

            if(_time <= 0) OnZero?.Invoke(this);
        }
    }

    public void Set(float second){
        _time = second;
    }

    public void Add(float second){
        _time += second;
    }
}
